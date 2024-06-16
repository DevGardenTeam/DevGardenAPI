using Auth;
using Microsoft.AspNetCore.Mvc;
using log4net;
using DevGardenAPI.Managers;
using DatabaseEf;
using DatabaseEf.Entities;
using Microsoft.EntityFrameworkCore;
using DatabaseEf.Controller;
using DatabaseEf.Responses;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthentificationController: ControllerBase
    {
        private readonly UsersController userController;
        private readonly UsersServiceController usersServiceController;

        public AuthentificationController(DataContext context)
        {
            userController = new UsersController(context);
            usersServiceController = new UsersServiceController(context);
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

            if(userController.UserExists(username))
            {
                return Conflict("User already exist");
            }

            var user = new User
            {   
                Username = username,
                Password = cryptedPassword,
                Email = username,
                UserServices = []
            };

            var result = await userController.PostUser(user);

            if (result != "Ok")
            {
                return Conflict(result);
            }

            return Ok("Register successful");
        }

        [HttpPost("login")]
        public async Task<AuthentificationResponse> Login(string username, string password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                return new AuthentificationResponse(BadRequest("Le mot de passe ne peut pas être vide ou seulement des espaces."), null, null);
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return new AuthentificationResponse(BadRequest("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces."), null, null);
            }

            var user = await userController.GetUserByUsername(username);

            if (user == null)
            {
                return new AuthentificationResponse(Unauthorized("Invalid username or password."), null, null);
            }

            if (!BcryptAuthHandler.VerifyPassword(password, EncryptionHelper.Decrypt(user.Password)))
            {
                return new AuthentificationResponse(Unauthorized("Invalid username or password."), null, null);
            }

            return new AuthentificationResponse(Ok("Login succesful"), username, await usersServiceController.GetUserServices(username));
        }

    }
}
