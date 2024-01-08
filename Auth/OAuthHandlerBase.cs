using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public abstract class OAuthHandlerBase
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ILogger _logger;

        protected OAuthHandlerBase(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public abstract Task<string> ExchangeToken(TokenRequest request);
    }
}
