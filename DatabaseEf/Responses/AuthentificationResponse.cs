using DatabaseEf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseEf.Responses
{
    public class AuthentificationResponse
    {
        public readonly IActionResult actionResult;

        public readonly String? username;

        public readonly List<UserService>? services;

        public AuthentificationResponse(IActionResult actionResult, string? username, List<UserService>? services)
        {
            this.actionResult = actionResult;
            this.username = username;
            this.services = services;
        }
    }
}
