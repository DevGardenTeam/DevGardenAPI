using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie File.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenFileController : ControllerBase
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
        /// Initialise une nouvelle instance de la classe <see cref="DevGardenFileController"/>.
        /// </summary>
        public DevGardenFileController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenFileController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFiles(string owner, string repository, string? path = null)
        {
            return await ExternalServiceManager.PlatformFileController.GetAllFiles(owner, repository, path);
        }

        #endregion
    }
}
