using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Branch.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Obtient le manager du service utilisé.
        /// </summary>
        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="BranchController"/>.
        /// </summary>
        public BranchController()
        {
            Logger = LogManager.GetLogger(typeof(BranchController));
        }


        [HttpGet("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches(string owner, string repository, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetAllBranches(owner, repository);
        }


        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch(string owner, string repository, string branch, string platform)
        {
            return await ExternalServiceManager.GetController(platform).GetBranch(owner, repository, branch);
        }
    }
}
