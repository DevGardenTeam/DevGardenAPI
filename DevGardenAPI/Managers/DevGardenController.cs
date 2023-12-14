using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DevGardenController : ControllerBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        public DevGardenController()
        {
            Logger = LogManager.GetLogger(typeof(DevGardenController));
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
            Logger.Debug($"{nameof(DevGardenController)} - {nameof(Get)} - Starting");

            try
            {
                string username = "LouBRODA";
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/users/{username}/repos";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(DevGardenController)} - {nameof(Get)} - Error");
                Logger.Error($"{nameof(Get)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
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
