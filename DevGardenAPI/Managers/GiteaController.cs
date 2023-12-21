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

        public override async Task<IActionResult> GetAllRepositories()
        {
            throw new NotImplementedException();
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
