using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    /// <summary>
    /// OAuth options.
    /// </summary>
    public class GithubOauthOptions
    {
        /// <summary>
        /// The oAuth app client id
        /// </summary>
        public string ClientId { get; set; } = string.Empty;

        /// <summary>
        /// The oAuth app client secret
        /// </summary>
        public string ClientSecret { get; set; } = string.Empty;
    }
}
