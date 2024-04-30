using Microsoft.AspNetCore.Mvc;
using Auth;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Controller to handle the OAuth authentication flow.
    /// </summary>
    [Route("/api/oauth")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IOAuthHandlerFactory _oauthHandlerFactory;

        public OAuthController(IOAuthHandlerFactory oauthHandlerFactory)
        {
            _oauthHandlerFactory = oauthHandlerFactory;
        }

        [HttpPost("token")]
        public async Task<IActionResult> ExchangeToken([FromBody] TokenRequest request, [FromQuery] string platform)
        {
            // create the appropriate instance of the oauth handler class depending on the given platform
            var oauthHandler = _oauthHandlerFactory.CreateHandler(platform);

            try
            {
                // attempt to get the exchange token
                string token = await oauthHandler.ExchangeToken(request);
                // return a json with the token if successfull
                Console.WriteLine("token :  " + token);
                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("test")]
        public async Task<string> GetTest()
        {
            return "test";
        }
    }
}
