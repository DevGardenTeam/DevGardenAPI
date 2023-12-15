using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenRepositoryController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

        public DevGardenRepositoryController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenMemberController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return await ExternalServiceManager.GithubMemberController.GetAllRepositories();
        }

        [HttpGet()]
        public async Task Get()
        {
        }

        [HttpPost]
        public async Task Post()
        {
        }

        [HttpPut]
        public async Task Put()
        {
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }

        #endregion
    }
}
