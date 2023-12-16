using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenBranchController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

        public DevGardenBranchController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenBranchController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAllBranches")]
        public async Task<IActionResult> GetAllBranches(string owner, string repository)
        {
            return await ExternalServiceManager.PlatformBranchController.GetAllBranches(owner, repository);
        }


        [HttpGet("GetBranch")]
        public async Task<IActionResult> GetBranch(string owner, string repository, string branch)
        {
            return await ExternalServiceManager.PlatformBranchController.GetBranch(owner, repository, branch);
        }

        #endregion
    }
}
