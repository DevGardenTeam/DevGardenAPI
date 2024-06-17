using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEf
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the user services.
        /// </summary>
        public DbSet<UserService> UserServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<UserService>().ToTable("user_services");

            // avoid mapping to int, and store the actual service name string
            modelBuilder.Entity<UserService>()
                .Property(entity => entity.ServiceName)
                .HasConversion<string>();

            // Composite primary key
            modelBuilder.Entity<UserService>()
                .HasKey(us => new { us.UserId, us.ServiceName });

            // one to many
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserServices)
                .WithOne()
                .HasForeignKey(us => us.UserId);
        }

        public async Task<string?> GetTokenByUsername(string username, ServiceName platform)
        {
            string? token = null;

            var userService = await Users
                .Where(u => u.Username == username)
                .SelectMany(u => u.UserServices)
                .FirstOrDefaultAsync(us => us.ServiceName == platform);

            if (userService != null)
            {
                token = userService.AccessToken;
            }

            return token;
        }
    }
}
