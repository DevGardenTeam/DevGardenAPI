using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    /// <summary>
    /// The OAuth handler factory interface.
    /// </summary>
    public interface IOAuthHandlerFactory
    {
        OAuthHandlerBase CreateHandler(string platform);
    }

    /// <summary>
    /// The OAuthHandlerFactory.
    /// </summary>
    public class OAuthHandlerFactory : IOAuthHandlerFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// The options such as client secret and id.
        /// </summary>
        private readonly OAuthClientOptions _clientOptions;

        public OAuthHandlerFactory(
            IHttpClientFactory httpClientFactory, 
            ILoggerFactory loggerFactory, 
            IOptions<OAuthClientOptions> clientOptions)
        {
            _httpClientFactory = httpClientFactory;
            _loggerFactory = loggerFactory;
            _clientOptions = clientOptions.Value;
        }

        /// <summary>
        /// Creates a OAuth handler for a given platform.
        /// </summary>
        /// <param name="platform">The platform.</param>
        /// <returns>The oAuth handler instance.</returns>
        /// <exception cref="ArgumentException"></exception>
        public OAuthHandlerBase CreateHandler(string platform)
        {
            switch (platform)
            {
                case "github":
                    return new GithubOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GithubOAuthHandler>(), _clientOptions) ;
                case "gitlab":
                    //return new GitlabOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GitlabOAuthHandler>());
                case "gitea":
                     return new GiteaOAuthHandler(_httpClientFactory, _loggerFactory.CreateLogger<GiteaOAuthHandler>(), _clientOptions);
                default:
                    throw new ArgumentException($"Invalid platform: {platform}");
            }
        }
    }
}
