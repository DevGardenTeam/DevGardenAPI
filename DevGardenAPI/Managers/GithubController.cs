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
    public class GithubController<T> : PlatformController<T> where T : ModelBase
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
        public override async Task<IActionResult> GetAllRepositories()
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
        public override async Task<IActionResult> GetActualRepository(string owner, string repository)
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetActualRepository)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}";

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

        #region Issue

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllIssues()
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetAllIssues)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/issues";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllIssues)} - Error");
                        Logger.Error($"{nameof(GetAllIssues)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllIssues)} - Error");
                Logger.Error($"{nameof(GetAllIssues)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #region Branch

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllBranches(string owner, string repository)
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetAllBranches)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}/branches";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllBranches)} - Error");
                        Logger.Error($"{nameof(GetAllBranches)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetAllBranches)} - Error");
                Logger.Error($"{nameof(GetAllBranches)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetBranch(string owner, string repository, string branch)
        {
            Logger.Debug($"{nameof(GithubController<T>)} - {nameof(GetBranch)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}/branches/{branch}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetBranch)} - Error");
                        Logger.Error($"{nameof(GetBranch)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController<T>)} - {nameof(GetBranch)} - Error");
                Logger.Error($"{nameof(GetBranch)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #endregion
    }
}
