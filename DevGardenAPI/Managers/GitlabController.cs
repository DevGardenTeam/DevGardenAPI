using DevGardenAPI.Adapter;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Contrôleur générique de la partie Gitlab implémentant les différentes méthodes définies dans le PlatformController.
    /// </summary>
    public class GitlabController : PlatformController
    {
        private readonly string gitlabApiStartUrl = "https://gitlab.com/api/v4";

        private PlatformAdapter platformAdapter;

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GitlabController"/>.
        /// </summary>
        public GitlabController()
        {
            this.platformAdapter = new GitlabAdapter();
            Logger = LogManager.GetLogger(typeof(GitlabController));
        }

        [HttpGet]
        public override async Task<List<Repository>> GetAllRepositories()
        {
            Logger.Debug(
                $"{nameof(GitlabController)} - {nameof(GetAllRepositories)} - Starting"
            );

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects?membership=true&statistics=true";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                       string json = await result.Content.ReadAsStringAsync();

                        // USE ADAPTER HERE TO DESERIALIZE

                        List<Repository> repositories = platformAdapter.ExtractRepositories(json);
                        return repositories;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetAllRepositories)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GitlabController)} - {nameof(GetAllRepositories)} - Error"
                );
                Logger.Error($"{nameof(GetAllRepositories)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetActualRepository(
            string owner,
            string repository
        )
        {
            Logger.Debug(
                $"{nameof(GitlabController)} - {nameof(GetActualRepository)} - Starting"
            );

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
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetActualRepository)} - Error"
                        );
                        Logger.Error($"{nameof(GetActualRepository)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GitlabController)} - {nameof(GetActualRepository)} - Error"
                );
                Logger.Error($"{nameof(GetActualRepository)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<List<Issue>> GetAllIssues(string owner, string repository)
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetAllIssues)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl = $"{gitlabApiStartUrl}/projects/{repository}/issues";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        var issues = platformAdapter.ExtractIssues(json);

                        return issues;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetAllIssues)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllIssues)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetAllIssues)} - Error");
                Logger.Error($"{nameof(GetAllIssues)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllBranches(
            string owner,
            string repository
        )
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetAllBranches)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl =
                        $"{gitlabApiStartUrl}/projects/{repository}/repository/branches";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetAllBranches)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllBranches)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetAllBranches)} - Error");
                Logger.Error($"{nameof(GetAllBranches)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetBranch(
            string owner,
            string repository,
            string branch
        )
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetBranch)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl =
                        $"{gitlabApiStartUrl}/projects/{repository}/repository/branches/{branch}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetBranch)} - Error"
                        );
                        Logger.Error($"{nameof(GetBranch)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetBranch)} - Error");
                Logger.Error($"{nameof(GetBranch)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<List<Commit>> GetAllCommits(string owner, string repository)
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetAllCommits)} - Starting");

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

                        var commits = platformAdapter.ExtractCommits(json);

                        return commits;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetAllCommits)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllCommits)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetAllCommits)} - Error");
                Logger.Error($"{nameof(GetAllCommits)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetCommit(
            string owner,
            string repository,
            string id
        )
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetCommit)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl =
                        $"{gitlabApiStartUrl}/projects/{repository}/repository/commits/{id}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetCommit)} - Error"
                        );
                        Logger.Error($"{nameof(GetCommit)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetCommit)} - Error");
                Logger.Error($"{nameof(GetCommit)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllFiles(
            string owner,
            string repository,
            string? path = null,
            bool isFolder = false
        )
        {
            Logger.Debug($"{nameof(GitlabController)} - {nameof(GetAllFiles)} - Starting");

            try
            {
                string token = "glpat-s6wALUpYoTt_fpzywGCp";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Private-Token", token);

                    string apiUrl;

                    if (path != null)
                    {
                        if(isFolder)
                        {
                            apiUrl =
                            $"{gitlabApiStartUrl}/projects/{repository}/repository/tree?path={path}";
                        } else
                        {
                            apiUrl =
                            $"{gitlabApiStartUrl}/projects/{repository}/repository/files/{path}/raw";
                        }
                        
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
                        Logger.Error(
                            $"{nameof(GitlabController)} - {nameof(GetAllFiles)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllFiles)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GitlabController)} - {nameof(GetAllFiles)} - Error");
                Logger.Error($"{nameof(GetAllFiles)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
