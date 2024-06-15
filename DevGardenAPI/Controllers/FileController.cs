using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie File.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
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
        /// Initialise une nouvelle instance de la classe <see cref="FileController"/>.
        /// </summary>
        public FileController()
        {
            Logger = LogManager.GetLogger(typeof(FileController));
        }

        [HttpGet("GetAllFiles")]
        public async Task<IActionResult> GetAllFiles(
            string owner,
            string repository,
            string platform,
            bool isFolder = false,
            string? path = null)
        {
            return await ExternalServiceManager.GetController(platform).GetAllFiles(
                owner,
                repository,
                path,
                isFolder
            );
        }
    }
}
