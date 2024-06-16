using Auth;
using Microsoft.AspNetCore.Mvc;
using DatabaseEf;
using DatabaseEf.Entities;
using DatabaseEf.Controller;
using DatabaseEf.Entities.Enums;

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
                return BadRequest(new
                {
                    message = "Le mot de passe ne peut pas être vide ou seulement des espaces.",
                });
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new
                {
                    message = "Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.",
                });
            }

            var user = await userController.GetUserByUsername(username);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password.",
                });
            }

            if (!BcryptAuthHandler.VerifyPassword(password, EncryptionHelper.Decrypt(user.Password)))
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password.",
                });
            }

            var userServices = await usersServiceController.GetUserServices(username);

            var serviceStatuses = new Dictionary<string, bool>();

            foreach (var serviceName in Enum.GetValues(typeof(ServiceName)))
            {
                // Check if the user has this service
                bool hasService = user.UserServices.Any(s => s.ServiceName.ToString().Equals(serviceName.ToString(), StringComparison.OrdinalIgnoreCase));

                // Add the result to the dictionary
                serviceStatuses.Add(serviceName.ToString().ToLower(), hasService);
            }

            // Return the modified response
            return Ok(new { isLogin = true, username = username, services = serviceStatuses });
        }

    }
}
