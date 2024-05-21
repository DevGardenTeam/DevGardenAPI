using System.Net;
using System.Text;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Contrôleur générique de la partie Github implémentant les différentes méthodes définies dans le PlatformController.
    /// </summary>
    public class GithubController : PlatformController
    {
        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GithubController"/>.
        /// </summary>
        public GithubController()
        {
            Logger = LogManager.GetLogger(typeof(GithubController));
        }

        [HttpGet]
        public override async Task<List<Repository>> GetAllRepositories()
        {
            Logger.Debug(
                $"{nameof(GithubController)} - {nameof(GetAllRepositories)} - Starting"
            );

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

                        Console.WriteLine(json);

                        List<Repository> repositories = JsonConvert.DeserializeObject<List<Repository>>(json);
                        return repositories;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetAllRepositories)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GithubController)} - {nameof(GetAllRepositories)} - Error"
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
                $"{nameof(GithubController)} - {nameof(GetActualRepository)} - Starting"
            );

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
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetActualRepository)} - Error"
                        );
                        Logger.Error($"{nameof(GetActualRepository)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GithubController)} - {nameof(GetActualRepository)} - Error"
                );
                Logger.Error($"{nameof(GetActualRepository)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<List<Issue>> GetAllIssues(string owner, string repository)
        {
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetAllIssues)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}/issues";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        List<Issue> issues = JsonConvert.DeserializeObject<List<Issue>>(json);
                        return issues;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetAllIssues)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllIssues)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetAllIssues)} - Error");
                Logger.Error($"{nameof(GetAllIssues)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllBranches(
            string owner,
            string repository,
            string token
        )
        {
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetAllBranches)} - Starting");

            try
            {
                //string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}/branches";
                    Console.WriteLine(token);

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetAllBranches)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllBranches)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetAllBranches)} - Error");
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
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetBranch)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl =
                        $"https://api.github.com/repos/{owner}/{repository}/branches/{branch}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetBranch)} - Error"
                        );
                        Logger.Error($"{nameof(GetBranch)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetBranch)} - Error");
                Logger.Error($"{nameof(GetBranch)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public override async Task<List<Commit>> GetAllCommits(string owner, string repository)
        {
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetAllCommits)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://api.github.com/repos/{owner}/{repository}/commits";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        Console.WriteLine(json);

                        List<Commit> commits = JsonConvert.DeserializeObject<List<Commit>>(json);
                        return commits;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetAllCommits)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllCommits)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetAllCommits)} - Error");
                Logger.Error($"{nameof(GetAllCommits)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public override async Task<IActionResult> GetCommit(
            string owner,
            string repository,
            string id
        )
        {
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetCommit)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl =
                        $"https://api.github.com/repos/{owner}/{repository}/commits/{id}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GithubController)} - {nameof(GetCommit)} - Error"
                        );
                        Logger.Error($"{nameof(GetCommit)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetCommit)} - Error");
                Logger.Error($"{nameof(GetCommit)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllFiles(
            string owner,
            string repository,
            string? path = null
        )
        {
            Logger.Debug($"{nameof(GithubController)} - {nameof(GetAllFiles)} - Starting");

            try
            {
                string token = "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";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl;

                    if (path != null)
                    {
                        apiUrl =
                            $"https://api.github.com/repos/{owner}/{repository}/contents/{path}";
                    }
                    else
                    {
                        apiUrl = $"https://api.github.com/repos/{owner}/{repository}/contents";
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
                            $"{nameof(GithubController)} - {nameof(GetAllFiles)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllFiles)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GithubController)} - {nameof(GetAllFiles)} - Error");
                Logger.Error($"{nameof(GetAllFiles)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
