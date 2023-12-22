using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Contrôleur générique de la partie Gitea implémentant les différentes méthodes définies dans le PlatformController.
    /// </summary>
    public class GiteaController<T> : PlatformController<T> where T : ModelBase
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="GiteaController"/>.
        /// </summary>
        public GiteaController()
        {
            Logger = LogManager.GetLogger(typeof(GiteaController<T>));
        }

        #endregion

        #region Methods

        #region Repository

        [ApiVersion("1.0")]
        [HttpGet]
        public override async Task<IActionResult> GetAllRepositories()
        {
            Logger.Debug($"{nameof(GiteaController<T>)} - {nameof(GetAllRepositories)} - Starting");

            try
            {
                string token = "50334ddce74b0605c1b71f38ace2d0854dd3570e";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "DevGarden");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    string apiUrl = $"https://gitea.com/api/v1/user/repos";

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        return Ok(json);
                    }
                    else
                    {
                        Logger.Error($"{nameof(GiteaController<T>)} - {nameof(GetAllRepositories)} - Error");
                        Logger.Error($"{nameof(GetAllRepositories)} - {result.StatusCode}");

                        return StatusCode((int)result.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{nameof(GiteaController<T>)} - {nameof(GetAllRepositories)} - Error");
                Logger.Error($"{nameof(GetAllRepositories)} - {ex.InnerException}");

                return null;//Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        public override async Task<IActionResult> GetActualRepository(string owner, string repository)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Issue

        public override async Task<IActionResult> GetAllIssues()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Branch

        public override async Task<IActionResult> GetAllBranches(string owner, string repository)
        {
            throw new NotImplementedException();
        }

        public override async Task<IActionResult> GetBranch(string owner, string repository, string branch)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Commit

        public override async Task<IActionResult> GetAllCommits(string owner, string repository)
        {
            throw new NotImplementedException();
        }

        public override async Task<IActionResult> GetCommit(string owner, string repository, string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
