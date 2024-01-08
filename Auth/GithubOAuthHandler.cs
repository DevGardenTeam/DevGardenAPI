using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Auth
{
    public class GithubOAuthHandler : OAuthHandlerBase
    {
        public GithubOAuthHandler(IHttpClientFactory httpClientFactory, ILogger logger) : base(httpClientFactory, logger)
        {
        }

        public override Task<string> ExchangeToken(TokenRequest request)
        {
            throw new NotImplementedException();
        }
    }
}