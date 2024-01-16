using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        private readonly GithubOauthOptions _clientOptions;

        public OAuthHandlerFactory(
            IHttpClientFactory httpClientFactory, 
            ILoggerFactory loggerFactory, 
            IOptions<GithubOauthOptions> clientOptions)
        {
            _httpClientFactory = httpClientFactory;
            _loggerFactory = loggerFactory;
            _clientOptions = clientOptions.Value;
        }

        public OAuthHandlerBase CreateHandler(string platform)
        {
            switch (platform)
            {
                case "github":
                    return new GithubOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GithubOAuthHandler>(), _clientOptions) ;
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
