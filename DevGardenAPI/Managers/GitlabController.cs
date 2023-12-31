using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Contrôleur générique de la partie Gitlab implémentant les différentes méthodes définies dans le PlatformController.
    /// </summary>
    public class GitlabController<T> : PlatformController<T> where T : ModelBase
    {
        #region Fields

        private readonly string gitlabApiStartUrl = "https://gitlab.com/api/v4";

        #endregion

        #region Properties

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GitlabController"/>.
        /// </summary>
        public GitlabController()
        {
            Logger = LogManager.GetLogger(typeof(GitlabController<T>));
        }

        #endregion

        #region Methods

        #region Repository

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllRepositories()
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetAllRepositories)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects?membership=true";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllRepositories)} - Error");
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllRepositories)} - Error");
                Logger.Error($"{nameof(GetAllRepositories)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetActualRepository(string owner, string repository)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetActualRepository)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetActualRepository)} - Error");
                        Logger.Error($"{nameof(GetActualRepository)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetActualRepository)} - Error");
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
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetAllIssues)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/issues";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllIssues)} - Error");
                        Logger.Error($"{nameof(GetAllIssues)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllIssues)} - Error");
                Logger.Error($"{nameof(GetAllIssues)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #region Branch

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllBranches(string owner, string repository, string token)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetAllBranches)} - Starting");

            try
            {
                //string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/branches";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllBranches)} - Error");
                        Logger.Error($"{nameof(GetAllBranches)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllBranches)} - Error");
                Logger.Error($"{nameof(GetAllBranches)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetBranch(string owner, string repository, string branch)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetBranch)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/branches/{branch}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetBranch)} - Error");
                        Logger.Error($"{nameof(GetBranch)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetBranch)} - Error");
                Logger.Error($"{nameof(GetBranch)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #region Commit

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllCommits(string owner, string repository)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetAllCommits)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/commits";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllCommits)} - Error");
                        Logger.Error($"{nameof(GetAllCommits)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllCommits)} - Error");
                Logger.Error($"{nameof(GetAllCommits)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetCommit(string owner, string repository, string id)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetCommit)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/commits/{id}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetCommit)} - Error");
                        Logger.Error($"{nameof(GetCommit)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetCommit)} - Error");
                Logger.Error($"{nameof(GetCommit)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #region File

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllFiles(string owner, string repository, string? path = null)
        {
            Logger.Debug($"{nameof(GitlabController<T>)} - {nameof(GetAllFiles)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl;

                    if (path != null)
                    {
                        apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/tree?path={path}";
                    }
                    else
                    {
                        apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/repository/tree";
                    } 

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllFiles)} - Error");
                        Logger.Error($"{nameof(GetAllFiles)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController<T>)} - {nameof(GetAllFiles)} - Error");
                Logger.Error($"{nameof(GetAllFiles)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion

        #endregion
    }
}
