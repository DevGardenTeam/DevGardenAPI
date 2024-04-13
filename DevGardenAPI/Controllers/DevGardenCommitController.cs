using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Commit.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DevGardenCommitController : ControllerBase
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
        /// Initialise une nouvelle instance de la classe <see cref="DevGardenCommitController"/>.
        /// </summary>
        public DevGardenCommitController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenCommitController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllCommits")]
        public async Task<IActionResult> GetAllCommits(string owner, string repository)
        {
            return await ExternalServiceManager.PlatformCommitController.GetAllCommits(owner, repository);
        }

        [HttpGet("GetCommit")]
        public async Task<IActionResult> GetCommit(string owner, string repository, string id)
        {
            return await ExternalServiceManager.PlatformCommitController.GetCommit(owner, repository, id);
        }

        #endregion
    }
}
