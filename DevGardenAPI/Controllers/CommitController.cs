﻿using DatabaseEf;
using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Commit.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommitController : ControllerBase
    {

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Obtient le manager du service utilisé.
        /// </summary>
        public ExternalServiceManager ExternalServiceManager { get; } 

         
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CommitController"/>.
        /// </summary>
        public CommitController(TokenService tokenService)
        {
            Logger = LogManager.GetLogger(typeof(CommitController));
            ExternalServiceManager = new ExternalServiceManager(tokenService);
        }

        [HttpGet("GetAllCommits")]
        public async Task<List<Commit>> GetAllCommits(string dgUsername, string owner, string repository, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetAllCommits(dgUsername, owner, repository);
        }


        [HttpGet("GetCommit")]
        public async Task<IActionResult> GetCommit(string owner, string repository, string id, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetCommit(owner, repository, id);
        }
    }
}
