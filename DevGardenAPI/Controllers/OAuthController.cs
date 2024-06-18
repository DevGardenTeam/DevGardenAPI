using Microsoft.AspNetCore.Mvc;
using Auth;
using DatabaseEf;
using DatabaseEf.Controller;
using DatabaseEf.Entities.Enums;
using DatabaseEf.Entities;
using Model;

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
        public UsersController userController;

        public OAuthController(IOAuthHandlerFactory oauthHandlerFactory, DataContext context)
        {
            _oauthHandlerFactory = oauthHandlerFactory;
            userController = new UsersController(context);
        }

        [HttpPost("token/exchange")]
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
                    AccessToken = EncryptionHelper.Encrypt(token),
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

        [HttpGet("tokens")]
        public async Task<IActionResult> GetAccessTokens(string username)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();

            var githubToken = await this.userController.GetService(username, ServiceName.github);
            var gitlabToken = await this.userController.GetService(username, ServiceName.gitea);
            var giteaToken = await this.userController.GetService(username, ServiceName.gitlab);

            if (githubToken != null) tokens.Add("github", EncryptionHelper.Decrypt(githubToken.AccessToken));
            if (gitlabToken != null) tokens.Add("gitlab", EncryptionHelper.Decrypt(gitlabToken.AccessToken)n);
            if (giteaToken != null) tokens.Add("gitea", EncryptionHelper.Decrypt(giteaToken.AccessToken));

            return Ok(new { tokens = tokens } );
        }
    }
}
