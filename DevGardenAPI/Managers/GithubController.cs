using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace DevGardenAPI.Managers
{ 
    public class GithubController<T> : ControllerBase where T : ModelBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        public GithubController()
        {
            Logger = LogManager.GetLogger(typeof(GithubController<T>));
        }

        #endregion

        #region Methods

        #region Repository

        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAllRepositories()
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetAllRepositories)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/user/repos";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllRepositories)} - Error");
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllRepositories)} - Error");
                Logger.Error($"{nameof(GetAllRepositories)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetActualRepository(string repos)
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetActualRepository)} - Starting");

            try
            {
                string username = "LouBRODA";
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{username}/{repos}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetActualRepository)} - Error");
                        Logger.Error($"{nameof(GetActualRepository)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetActualRepository)} - Error");
                Logger.Error($"{nameof(GetActualRepository)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #endregion
    }
}
