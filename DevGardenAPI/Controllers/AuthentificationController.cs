using Auth;
using Microsoft.AspNetCore.Mvc;
using log4net;
using DevGardenAPI.Managers;
using DatabaseEf;
using DatabaseEf.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthentificationController: ControllerBase
    {
        private readonly DataContext _context;

        public AuthentificationController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(string username, string password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }

            if (!BcryptAuthHandler.IsPasswordComplexEnough(password))
            {
                return BadRequest("Le mot de passe n'est pas assez long ou ne comporte pas les caractères nécessaires. (12 charactères avec minuscule, majuscule, chiffre, caratère spécial)");
            }

            string cryptedPassword = EncryptionHelper.Encrypt(BcryptAuthHandler.HashPassword(password));

            var user = new User
            {   
                Username = username,
                Password = cryptedPassword
            };

            try {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(await _context.Users.FindAsync(username));
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Le mot de passe ne peut pas être vide ou seulement des espaces.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            if (!BcryptAuthHandler.VerifyPassword(password, EncryptionHelper.Decrypt(user.Password)))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("Login successful.");
        }

    }
}
