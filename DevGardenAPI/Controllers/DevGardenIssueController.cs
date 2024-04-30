using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Issue.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DevGardenIssueController : ControllerBase
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
        /// Initialise une nouvelle instance de la classe <see cref="DevGardenIssueController"/>.
        /// </summary>
        public DevGardenIssueController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenIssueController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllIssues")]
        public async Task<List<Issue>> GetAllIssues(string owner, string repository)
        {
            return await ExternalServiceManager.PlatformIssueController.GetAllIssues(owner, repository);
        }

        #endregion
    }
}
