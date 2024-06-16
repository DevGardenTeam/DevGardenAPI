using Microsoft.AspNetCore.Mvc;
using Auth;
using DatabaseEf;
using DatabaseEf.Controller;
using DatabaseEf.Entities.Enums;
using DatabaseEf.Entities;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Controller to handle the OAuth authentication flow.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly IOAuthHandlerFactory _oauthHandlerFactory;
        private readonly UsersController userController;

        public OAuthController(IOAuthHandlerFactory oauthHandlerFactory, DataContext context)
        {
            _oauthHandlerFactory = oauthHandlerFactory;
            userController = new UsersController(context);
        }

        [HttpPost("token")]
        public async Task<IActionResult> ExchangeToken([FromBody] TokenRequest request, [FromQuery] string platform, string username)
        {
            // create the appropriate instance of the oauth handler class depending on the given platform
            var oauthHandler = _oauthHandlerFactory.CreateHandler(platform);

            try
            {
                // attempt to get the exchange token
                string token = await oauthHandler.ExchangeToken(request);

                // return a json with the token if successfull
                Console.WriteLine("token :  " + token);

                if (!Enum.TryParse(platform, true, out ServiceName servicename))
                {
                    return BadRequest("Invalid service name");
                }

                // create a new service instance with the token
                var newService = new UserService
                {
                    AccessToken = token,
                    ServiceName = servicename
                };

                // add the service to the user in the database
                var result = await userController.AddService(username, newService);

                // return the result
                return Ok(new { isLinked = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error : '{ex.Message}");
            }
        }

        [HttpGet("test")]
        public async Task<string> GetTest()
        {
            return "test";
        }
    }
}
