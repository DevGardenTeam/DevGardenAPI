﻿using DatabaseEf;
using DatabaseEf.Entities.Enums;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Contrôleur générique de la partie Gitea implémentant les différentes méthodes définies dans le PlatformController.
    /// </summary>
    public class GiteaController : PlatformController
    {
        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        private readonly TokenService _tokenService;
        const ServiceName serviceName = ServiceName.gitea;


        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GiteaController"/>.
        /// </summary>
        public GiteaController(TokenService tokenService)
        {
            Logger = LogManager.GetLogger(typeof(GiteaController));
            _tokenService = tokenService;
        }

        [HttpGet]
        public override async Task<List<Repository>> GetAllRepositories(string dgUsername)
        {
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetAllRepositories)} - Starting");

            try
            {
                var token = await _tokenService.GetTokenAsync(dgUsername, serviceName);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://gitea.com/api/v1/user/repos";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        Console.WriteLine(json);

                        List<Repository> repositories = JsonConvert.DeserializeObject<
                            List<Repository>
                        >(json);
                        return repositories;
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GiteaController)} - {nameof(GetAllRepositories)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GiteaController)} - {nameof(GetAllRepositories)} - Error"
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
                $"{nameof(GiteaController)} - {nameof(GetActualRepository)} - Starting"
            );

            try
            {
                string token = "50334ddce74b0605c1b71f38ace2d0854dd3570e";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://gitea.com/api/v1/user/repos/{owner}/{repository}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GiteaController)} - {nameof(GetActualRepository)} - Error"
                        );
                        Logger.Error($"{nameof(GetActualRepository)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(
                    $"{nameof(GiteaController)} - {nameof(GetActualRepository)} - Error"
                );
                Logger.Error($"{nameof(GetActualRepository)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<List<Issue>> GetAllIssues(string dgUsername, string owner, string repository)
        {
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetAllIssues)} - Starting");

            try
            {
                var token = await _tokenService.GetTokenAsync(dgUsername, serviceName);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://gitea.com/api/v1/repos/{owner}/{repository}/issues";

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
                            $"{nameof(GiteaController)} - {nameof(GetAllIssues)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllIssues)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetAllIssues)} - Error");
                Logger.Error($"{nameof(GetAllIssues)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllBranches(
            string dgUsername,
            string owner,
            string repository
        )
        {
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetAllBranches)} - Starting");

            try
            {
                var token = await _tokenService.GetTokenAsync(dgUsername, serviceName);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://gitea.com/api/v1/repos/{owner}/{repository}/branches";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        // create a list of branches
                        var branches = JsonConvert.DeserializeObject<List<Branch>>(json);

                        foreach (var branch in branches)
                        {
                            var commits = await this.GetAllCommits(dgUsername, owner, repository, branch.Name);
                            branch.Commits = commits;
                        }

                        return Ok(branches);
                    }
                    else
                    {
                        Logger.Error(
                            $"{nameof(GiteaController)} - {nameof(GetAllBranches)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllBranches)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetAllBranches)} - Error");
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
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetBranch)} - Starting");

            try
            {
                string token = "50334ddce74b0605c1b71f38ace2d0854dd3570e";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl =
                        $"https://gitea.com/api/v1/repos/{owner}/{repository}/branches/{branch}";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GiteaController)} - {nameof(GetBranch)} - Error");
                        Logger.Error($"{nameof(GetBranch)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetBranch)} - Error");
                Logger.Error($"{nameof(GetBranch)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<List<Commit>> GetAllCommits(string dgUsername, string owner, string repository, string? branch)
        {
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetAllCommits)} - Starting");

            try
            {
                string token = "50334ddce74b0605c1b71f38ace2d0854dd3570e";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = string.Empty;
                    if (branch == null)
                    {
                        apiUrl = $"https://gitea.com/api/v1/repos/{owner}/{repository}/commits";
                    }
                    else
                    {
                        apiUrl = $"https://gitea.com/api/v1/repos/{owner}/{repository}/commits?sha={branch}";
                    }

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
                            $"{nameof(GiteaController)} - {nameof(GetAllCommits)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllCommits)} - {result.StatusCode}");

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetAllCommits)} - Error");
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
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetCommit)} - Starting");

            try
            {
                string token = "50334ddce74b0605c1b71f38ace2d0854dd3570e";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl =
                        $"https://gitea.com/api/v1/repos/{owner}/{repository}/commits/{id}/status";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GiteaController)} - {nameof(GetCommit)} - Error");
                        Logger.Error($"{nameof(GetCommit)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetCommit)} - Error");
                Logger.Error($"{nameof(GetCommit)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public override async Task<IActionResult> GetAllFiles(
            string dgUsername,
            string owner,
            string repository,
            string? path = null,
            bool isFolder = false
        )
        {
            Logger.Debug($"{nameof(GiteaController)} - {nameof(GetAllFiles)} - Starting");

            try
            {
                var token = await _tokenService.GetTokenAsync(dgUsername, serviceName);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl;

                    if (path != null)
                    {
                        apiUrl =
                            $"https://gitea.com/api/v1/repos/{owner}/{repository}/contents/{path}";
                    }
                    else
                    {
                        apiUrl = $"https://gitea.com/api/v1/repos/{owner}/{repository}/contents";
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
                            $"{nameof(GiteaController)} - {nameof(GetAllFiles)} - Error"
                        );
                        Logger.Error($"{nameof(GetAllFiles)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController)} - {nameof(GetAllFiles)} - Error");
                Logger.Error($"{nameof(GetAllFiles)} - {ex.InnerException}");

                return null; //Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
