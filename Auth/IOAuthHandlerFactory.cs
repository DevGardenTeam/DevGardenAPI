using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public interface IOAuthHandlerFactory
    {
        OAuthHandlerBase CreateHandler(string platform);
    }

    public class OAuthHandlerFactory : IOAuthHandlerFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerFactory _loggerFactory;

        public OAuthHandlerFactory(IHttpClientFactory httpClientFactory, ILoggerFactory loggerFactory)
        {
            _httpClientFactory = httpClientFactory;
            _loggerFactory = loggerFactory;
        }

        public OAuthHandlerBase CreateHandler(string platform)
        {
            switch (platform)
            {
                case "github":
                    return new GithubOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GithubOAuthHandler>());
                case "gitlab":
                    //return new GitlabOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GitlabOAuthHandler>());
                case "gitea":
                    // return new GiteaOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GiteaOAuthHandler>());
                default:
                    throw new ArgumentException($"Invalid platform: {platform}");
            }
        }
    }
}
