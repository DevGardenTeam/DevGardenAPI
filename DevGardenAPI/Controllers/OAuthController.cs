using Microsoft.AspNetCore.Mvc;
using Auth;

namespace DevGardenAPI.Controllers
{
    /*
     * Controller that handles the OAuth flow.
     * POC for now, maybe a more concrete version needs to be implemented.
     */
    [Route("api/v{version:apiVersion}/oauth")]
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
            var oauthHandler = _oauthHandlerFactory.CreateHandler(platform);

            try
            {
                string token = await oauthHandler.ExchangeToken(request);
                return Ok(new { accesstoken = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
