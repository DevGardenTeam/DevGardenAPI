using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Auth
{
    public class GithubOAuthHandler // : IOAuthHandler
    {
        /*private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<GithubOAuthHandler> _logger;

        public GithubOAuthHandler(IHttpClientFactory httpClientFactory, ILogger<GithubOAuthHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async  Task<IActionResult> ExchangeToken(TokenRequest request)
        {
            _logger.LogInformation($"Received request with code: {request.Code}");

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

                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
        }*/
    }
}