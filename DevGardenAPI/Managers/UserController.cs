using DatabaseEf;
using Microsoft.AspNetCore.Mvc;
using DatabaseEf.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGardenAPI.Managers
{
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        public IActionResult GetUserPasswordByUsername(string username)
        {
            var user = _context.Users
                .Include(u => u.UserServices)
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.Password);
        }
    }
}
