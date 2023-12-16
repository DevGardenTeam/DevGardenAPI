using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenCommitController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

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
