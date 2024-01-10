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
            var token = await oauthHandler.ExchangeToken(request);
        }

        /*private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OAuthController> _logger;

        public OAuthController(IHttpClientFactory httpClientFactory, ILogger<OAuthController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpPost("token")]
        public async Task<IActionResult> ExchangeToken([FromBody] TokenRequest request)
        {
            _logger.LogInformation($"Received request with code: {request?.Code}");
            Console.WriteLine($"Received request with code: {request?.Code}");

            try
            {
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    // [TODO] Move this to a config file in a safe way .
                    new KeyValuePair<string, string>("client_id", "e2ab8ffbefc5b983f71b"),
                    new KeyValuePair<string, string>("client_secret", "85fa33682f94f8f98fea2c03b499f403c6ed90b4"),
                    new KeyValuePair<string, string>("code", request.Code),
                    new KeyValuePair<string, string>("redirect_uri", "http://localhost:19006/auth/callback"),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                });

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.PostAsync("https://github.com/login/oauth/access_token", requestBody);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Read the response RAW
                    _logger.LogInformation($"Raw Response Content: {responseContent}");


                    var accessToken = ExtractAccessToken(responseContent);

                    // Error handling
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        _logger.LogInformation($"Access token received: {accessToken}");

                        // Return an OK response with the access token as JSON object
                        return Ok(new { access_token = accessToken });
                    }
                    else
                    {
                        _logger.LogError("Failed to extract access token from response.");
                        return StatusCode(500, new { error = "Internal Server Error" });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in token exchange: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /*
         * Extracts the access token from the response content.
         * I made this based on the raw format when testing with GitHub API.
         * [TODO] Maybe a different version depending on the service (Gitea, Gitlab...).
         */
        /*private string ExtractAccessToken(string responseContent)
        {
            // Gets the value of a query string parameter from the response content.
            // example: access_token=123456789&scope=repo%2Cgist&token_type=bearer
            // queryString["access_token"] will return 123456789
            var queryString = System.Web.HttpUtility.ParseQueryString(responseContent);
            return queryString["access_token"];
        }
    }*/
    }
