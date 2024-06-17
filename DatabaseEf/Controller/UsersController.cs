using Azure.Core;
using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseEf.Controller
{
    public class UsersController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        public async Task<string> PostUser(User user)
        {
            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return e.Message;
            }

            return "Ok";
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var user = await _context.Users.FindAsync(username);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetUserId(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return -1;
            }

            return user.Id;
        }

        public async Task<bool> AddService(string username, UserService newUserService)
        {
            var user = await _context.Users.Include(u => u.UserServices)
                                            .SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return false;
            }

            var existingService = user.UserServices
                                       .SingleOrDefault(us => us.ServiceName == newUserService.ServiceName);

            if (existingService != null)
            {
                // Update existing service's AccessToken
                existingService.AccessToken = newUserService.AccessToken;
            }
            else
            {
                // Create new UserService
                newUserService.UserId = user.Id;
                user.UserServices.Add(newUserService);
            }

            await _context.SaveChangesAsync();

            return true;
        }


        public bool UserExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }

        public async Task<List<UserService>> GetUserServices(string username)
        {
            var userController = new UsersController(_context);

            var result = await userController.GetUserId(username);

            if (result == -1)
            {
                throw new Exception("Failed to found the user on the database");
            }

            var userServices = await _context.UserServices
                                             .Where(us => us.UserId == result)
                                             .ToListAsync();

            return userServices;
        }

        public async Task<UserService?> GetService(string username, ServiceName service)
        {
            var userController = new UsersController(_context);

            var result = await userController.GetUserId(username);

            if (result == -1)
            {
                throw new Exception("Failed to found the user on the database");
            }

            var userService = await _context.UserServices
                                             .SingleOrDefaultAsync(us => us.UserId == result && us.ServiceName == service);

            return userService;
        }
    }
}
