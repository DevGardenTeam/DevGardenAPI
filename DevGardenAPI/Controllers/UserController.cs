using DatabaseEf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User = DatabaseEf.Entities.User;

namespace DevGardenAPI.Controllers
{
    public class UserController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<string>>> GetAllUsername()
        {
            var users = await _dataContext.Users.ToListAsync();

            List<string> list = [];

            foreach (var user in users)
            {
                list.Add(user.Username);
            }

            return Ok(list);
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _dataContext.Users.FindAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetUser(user.Username));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUsername(string username)
        {
            var dbUser = await _dataContext.Users.FindAsync(username);
            if (dbUser == null) {
                return NotFound("User not found");
            }

            dbUser.Username = username;
            await _dataContext.SaveChangesAsync();

            return Ok(await GetUser(username));
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser(string username)
        {
            var dbUser = await _dataContext.Users.FindAsync(username);
            if (dbUser == null)
            {
                return NotFound("User not found");
            }

            _dataContext.Remove(dbUser);
            await _dataContext.SaveChangesAsync();

            return Ok(await GetUser(username));
        }
    }
}
