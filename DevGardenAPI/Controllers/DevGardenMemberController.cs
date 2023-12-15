using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DevGardenMemberController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        public ExternalServiceManager ExternalServiceManager { get; } = new ExternalServiceManager();

        #endregion

        #region Constructor

        public DevGardenMemberController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenMemberController));
        }

        #endregion

        #region Methods

        [HttpGet("GetAll")]
        public async Task GetAll()
        {
        }

        [HttpGet()]
        public async Task<HttpResponseMessage> Get()
        {
            return await ExternalServiceManager.GithubMemberController.Get();
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
