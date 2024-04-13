using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Repository.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DevGardenRepositoryController : ControllerBase
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
        /// Initialise une nouvelle instance de la classe <see cref="DevGardenRepositoryController"/>.
        /// </summary>
        public DevGardenRepositoryController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenRepositoryController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllRepositories")]
        public async Task<IActionResult> GetAllRepositories()
        {
            return await ExternalServiceManager.PlatformRepositoryController.GetAllRepositories();
        }

        [HttpGet("GetActualRepository")]
        public async Task<IActionResult> GetActualRepository(string owner, string repos)
        {
            return await ExternalServiceManager.PlatformRepositoryController.GetActualRepository(owner, repos);
        }

        #endregion
    }
}
