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
    public class OAuthClientOptions
    {
        public Dictionary<string, string> ClientIds;
        public Dictionary<string, string> ClientSecrets;
    }
}
