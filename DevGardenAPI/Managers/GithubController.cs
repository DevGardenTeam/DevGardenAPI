using DevGardenAPI.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DevGardenAPI.Managers
{ 
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/git/[controller]")]
    public class GithubController<T> : DevGardenController<T> where T : ModelBase
    {
        #region Properties

        #endregion

        #region Constructor
        
        public GithubController(IRepository<T> repository) : base(repository)
        {
        }

        #endregion

        #region Methods

        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(Get)} - Starting");

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
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(Get)} - Error");
                Logger.Error($"{nameof(Get)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion
    }
}
