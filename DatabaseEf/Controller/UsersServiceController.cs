using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<UserService?> GetService(string username,ServiceName service){
            var userController = new UsersController(_context);

            var result = await userController.GetUserId(username);

            if (result == -1)
            {
                return null;
            }

            var userService = await _context.UserServices
                                             .SingleOrDefaultAsync(us => us.UserId == result && us.ServiceName == service);

            return userService;
        }
    }
}
