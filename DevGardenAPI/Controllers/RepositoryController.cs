using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Repository.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RepositoryController : ControllerBase
    {
        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Obtient le manager du service utilisé.
        /// </summary>
        public ExternalServiceManager ExternalServiceManager { get; } =
            new ExternalServiceManager();

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RepositoryController"/>.
        /// </summary>
        public RepositoryController()
        {
            Logger = LogManager.GetLogger(typeof(RepositoryController));
        }

        [HttpGet("GetAllRepositories")]
        public async Task<List<Repository>> GetAllRepositories(string dgUsername, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetAllRepositories(dgUsername);
        }

        [HttpGet("GetActualRepository")]
        public async Task<IActionResult> GetActualRepository(string owner, string repos, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetActualRepository(
                owner,
                repos
            );
        }
    }
}
