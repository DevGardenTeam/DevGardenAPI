using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Auth
{
    /// <summary>
    /// The github oAuth handler.
    /// </summary>
    public class GithubOAuthHandler : OAuthHandlerBase
    {
        public GithubOAuthHandler(
            IHttpClientFactory httpClientFactory, ILogger logger, GithubOauthOptions options) 
            : base(httpClientFactory, logger, options)
        {
        }

        /// <summary>
        /// Make the authentication token exchange.
        /// </summary>
        /// <param name="request">The token request.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override async Task<string> ExchangeToken(TokenRequest request)
        {
            _logger.LogInformation($"Received request with code: {request?.Code}");
            Console.WriteLine($"Received request with code: {request?.Code}");

            try
            {
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    // [TODO] Move this to a config file in a safe way .
                    new KeyValuePair<string, string>("client_id", this._clientOptions.ClientId),
                    new KeyValuePair<string, string>("client_secret", this._clientOptions.ClientSecret),
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
        /// Extracts the access token from the github api response.
        /// </summary>
        /// <param name="responseContent"></param>
        /// <returns></returns>
        private string ExtractAccessToken(string responseContent)
        {
            // Gets the value of a query string parameter from the response content.
            // example: access_token=123456789&scope=repo%2Cgist&token_type=bearer
            // queryString["access_token"] will return 123456789
            var queryString = System.Web.HttpUtility.ParseQueryString(responseContent);
            var access_token = queryString["access_token"];

            // check if access token was found
            if(access_token == null)
            {
                throw new Exception("The exchange token wasn't found on the external api's response.");
            } 

            return access_token;
        }
    }
}