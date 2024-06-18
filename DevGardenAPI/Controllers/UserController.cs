using DatabaseEf.Controller;
using DatabaseEf;
using Microsoft.AspNetCore.Mvc;
using DatabaseEf.Entities;
using DatabaseEf.Entities.Enums;
using Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Issue.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private UsersController userController { get; }

        private UsersServiceController UsersServiceController { get; }

        private DBController dbController { get; }

        public UserController(DataContext context)
        {
            userController = new UsersController(context);
            UsersServiceController = new UsersServiceController(context);
            dbController = new DBController(context);
        }

        [HttpGet("getServices")]
        public async Task<List<UserService>> GetUserServices(string username)
        {

            var listUserService = await UsersServiceController.GetUserServices(username);

            foreach (var userService in listUserService)
            {
                userService.AccessToken = EncryptionHelper.Decrypt(userService.AccessToken);
            }

            return listUserService;
        }

        [HttpGet("getService")]
        public async Task<UserService> GetUserService(string username, ServiceName service)
        {

            username = BcryptAuthHandler.CleanUsername(username);

            var usersevice = await UsersServiceController.GetService(username, service);

            if (usersevice == null)
            {
                return null;
            }

            usersevice.AccessToken = EncryptionHelper.Decrypt(usersevice.AccessToken);

            return usersevice;
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
                AccessToken = EncryptionHelper.Encrypt(token),
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

        [HttpPost("removeService")]
        public async Task<IActionResult> removeUserService(string username, ServiceName service)
        {
            username = BcryptAuthHandler.CleanUsername(username);

            await UsersServiceController.removeService(username, service);

            return Ok("Votre service vient d'être supprimé");
        }

        [HttpPost("flushDB")]
        public async Task<IActionResult> flushDB()
        {

            try
            {
                await dbController.FlushDB();
            }
            catch (Exception ex) {
                Conflict(ex.Message);
            }

            return Ok("Votre service vient d'être supprimé");
        }
    }
}
