using DatabaseEf.Controller;
using DatabaseEf;
using Microsoft.AspNetCore.Mvc;
using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
using Auth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Issue.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        public UsersController userController;

        public UsersServiceController UsersServiceController { get; set; }

        public UserController(DataContext context)
        {
            userController = new UsersController(context);
            UsersServiceController = new UsersServiceController(context);
        }

        [HttpGet("getServices")]
        public async Task<List<UserService>> GetUserServices(string username)
        {

            var listUserService = await UsersServiceController.GetUserServices(username);

            return listUserService;
        }

        [HttpPost("AddServices")]
        public async Task<IActionResult> AddUserService(string username, string token, string service)
        {

            username = BcryptAuthHandler.CleanUsername(username);
            token = BcryptAuthHandler.CleanPassword(token);
            service = BcryptAuthHandler.CleanPassword(service);

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Le nom d'utilisateur ne peut pas être vide ou seulement des espaces.");
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Le token ne peut pas être vide ou seulement des espaces.");
            }
            if (string.IsNullOrWhiteSpace(service))
            {
                return BadRequest("Le nom du service ne peut pas être vide ou seulement des espaces.");
            }

            if (!Enum.TryParse<ServiceName>(service, true, out ServiceName servicename))
            {
                return BadRequest("Invalid service name");
            }

            var newService = new UserService
            {
                AccessToken = token,
                ServiceName = servicename
            };

            // if (!userController.UserExists(username)){
            //     return Conflict("Erreur : le user n'existe pas");
            // }

            if (!await userController.AddService(username, newService))
            {
                return Conflict("Erreur dans l'ajout du Service");

            }
            return Ok("Votre token a été ajouté !!");
        }
    }
}
