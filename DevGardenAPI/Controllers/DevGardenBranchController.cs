using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Branch.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenBranchController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Obtient le manager du service utilisé.
        /// </summary>
        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DevGardenBranchController"/>.
        /// </summary>
        public DevGardenBranchController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenBranchController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches(string owner, string repository, string token)
        {
            return await ExternalServiceManager.PlatformBranchController.GetAllBranches(owner, repository, token);
        }


        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch(string owner, string repository, string branch)
        {
            return await ExternalServiceManager.PlatformBranchController.GetBranch(owner, repository, branch);
        }

        #endregion
    }
}
