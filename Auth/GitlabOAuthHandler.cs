using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class GitlabOAuthHandler : OAuthHandlerBase
    {
        public GitlabOAuthHandler(IHttpClientFactory httpClientFactory, ILogger logger, OAuthClientOptions options) : base(httpClientFactory, logger, options)
        {
        }

        public override async Task<string> ExchangeToken(TokenRequest request)
        {
            _logger.LogInformation($"Received request with code: {request?.Code}");
            Console.WriteLine($"Received request with code: {request?.Code}");

            try
            {
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", this._clientOptions.ClientIds["gitlab"]),
                    new KeyValuePair<string, string>("client_secret", this._clientOptions.ClientSecrets["gitlab"]),
                    new KeyValuePair<string, string>("code", request.Code),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("redirect_uri", "http://localhost:19006/auth/callback"),
                });

                _logger.LogInformation($"Request body: {await requestBody.ReadAsStringAsync()}");

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.PostAsync("https://gitlab.com/oauth/token", requestBody);

                    Console.WriteLine($"Response: {response}");

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
                        return accessToken;
                    }
                    else
                    {
                        _logger.LogError("Failed to extract access token from response.");
                        throw new Exception("Failed to extract access token from response.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in token exchange: {ex.Message}");
                throw new Exception($"Exception happened during the token exchange: {ex.Message}");
            }
        }

        /// <summary>
        /// Extracts the access token from the gitlab api response.
        /// </summary>
        /// <param name="responseContent"></param>
        /// <returns></returns>
        private string ExtractAccessToken(string responseContent)
        {
            try
            {
                // Parse JSON directly
                JObject json = JObject.Parse(responseContent);

                // Get the access_token value from the JSON
                var access_token = (string)json["access_token"];

                // check if access token was found
                if (access_token == null)
                {
                    throw new Exception("The exchange token wasn't found on the external API's response.");
                }

                return access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing JSON: " + ex.Message);
                throw;
            }
        }
    }
}
