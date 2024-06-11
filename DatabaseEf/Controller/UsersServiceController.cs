using DatabaseEf.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEf.Controller
{
    public class UsersServiceController
    {
        private readonly DataContext _context;

        public UsersServiceController(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UserService>> GetUserServices(string username)
        {
            var userController = new UsersController(_context);

            var result = await userController.GetUserId(username);

            if (result == -1)
            {
                return [];
            }

            var userServices = await _context.UserServices
                                             .Where(us => us.UserId == result)
                                             .ToListAsync();

            return userServices;
        }
    
    }
}
