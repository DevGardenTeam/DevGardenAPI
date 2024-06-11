using DatabaseEf.Entities;
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


        public bool UserExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
