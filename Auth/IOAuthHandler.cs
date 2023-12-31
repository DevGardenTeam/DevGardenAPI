using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public interface IOAuthHandler
    {
        // method to perform the client token exchange
        Task<IActionResult> ExchangeToken(TokenRequest request); 
    }
}
