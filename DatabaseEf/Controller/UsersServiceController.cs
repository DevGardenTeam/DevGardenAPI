using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
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
                throw new Exception("Failed to found the user on the database");
            }

            var userServices = await _context.UserServices
                                             .Where(us => us.UserId == result)
                                             .ToListAsync();

            return userServices;
        }

        public async Task<UserService> GetService(string username, ServiceName service){
            var userController = new UsersController(_context);

            var result = await userController.GetUserId(username);

            if (result == -1)
            {
                throw new Exception("Failed to found the user on the database");
            }

            var userService = await _context.UserServices
                                             .SingleOrDefaultAsync(us => us.UserId == result && us.ServiceName == service);

            if(userService == null)
            {
                throw new Exception("Failed to retrieve the user's service for: " + service.ToString());
            }

            return userService;
        }
    }
}
