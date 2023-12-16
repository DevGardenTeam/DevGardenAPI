using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    public abstract class PlatformController<T> : ControllerBase where T : ModelBase
    {
        #region Methods

        #region Repository

        public abstract Task<IActionResult> GetAllRepositories();

        public abstract Task<IActionResult> GetActualRepository(string owner, string repos);

        #endregion

        #region Issue

        public abstract Task<IActionResult> GetAllIssues();

        #endregion

        #region Branch

        public abstract Task<IActionResult> GetAllBranches(string owner, string repository);

        public abstract Task<IActionResult> GetBranch(string owner, string repository, string branch);

        #endregion

        #region Commit

        public abstract Task<IActionResult> GetAllCommits(string owner, string repository);

        public abstract Task<IActionResult> GetCommit(string owner, string repository, string id);

        #endregion

        #endregion
    }
}
