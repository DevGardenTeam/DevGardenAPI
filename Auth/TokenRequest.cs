using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    /*
     * This class is used to deserialize the response from the GitHub API.
     */
    public class TokenRequest
    {
        public string Code { get; set; }
    }
}
