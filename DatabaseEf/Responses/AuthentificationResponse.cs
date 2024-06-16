using DatabaseEf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseEf.Responses
{
    public class AuthentificationResponse(String actionResult, string? username, List<UserService>? services)
    {
        public readonly String actionResult = actionResult;

        public readonly String? username = username;

        public readonly List<UserService>? services = services;
    }
}
