using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DevGardenAPI.Controllers
{
    /*
     * 
     */
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OAuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("token")]
        public async Task<IActionResult> ExchangeToken([FromBody] TokenRequest request)
        {
            var requestBody = new FormUrlEncodedContent(new[]
            {
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
                var responseData = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                return Ok(responseData);
            }
        }
    }
}
