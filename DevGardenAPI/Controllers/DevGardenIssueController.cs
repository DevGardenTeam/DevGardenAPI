using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenIssueController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

        public DevGardenIssueController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenIssueController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllIssues")]
        public async Task<IActionResult> GetAllIssues()
        {
            return await ExternalServiceManager.PlatformIssueController.GetAllIssues();
        }

        #endregion
    }
}
