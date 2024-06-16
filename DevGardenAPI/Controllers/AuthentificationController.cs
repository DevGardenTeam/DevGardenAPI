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
        public async Task<IActionResult> Login(string username, string password)
        {
            username = BcryptAuthHandler.CleanUsername(username);
            password = BcryptAuthHandler.CleanPassword(password);

            if (string.IsNullOrWhiteSpace(password))
            {
                var response = new AuthentificationResponse("Le mot de passe ne peut pas être vide ou seulement des espaces.", null, null);
                return BadRequest(response);
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                var response = new AuthentificationResponse("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.", null, null);
                return BadRequest(response);
            }

            var user = await userController.GetUserByUsername(username);

            if (user == null)
            {
                var response = new AuthentificationResponse("Invalid username or password.", null, null);
                return Unauthorized(new JsonResult(response));
            }

            if (!BcryptAuthHandler.VerifyPassword(password, EncryptionHelper.Decrypt(user.Password)))
            {
                var response = new AuthentificationResponse("Invalid username or password.", null, null);
                return Unauthorized(new JsonResult(response));
            }

            var userServices = await usersServiceController.GetUserServices(username);
            var successResponse = new AuthentificationResponse("Login successful", username, userServices);
            return Ok(successResponse);
        }

    }
}
