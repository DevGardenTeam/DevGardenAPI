using Model;

namespace DevGardenAPI.Managers
{
    public class ExternalServiceManager
    {
        #region Properties

        #region Repository

        public PlatformController<Member> PlatformMemberController { get; private set; }

        public PlatformController<Repository> PlatformRepositoryController { get; private set; }

        public PlatformController<Issue> PlatformIssueController { get; private set; }

        public PlatformController<Branch> PlatformBranchController { get; private set; }

        public PlatformController<Commit> PlatformCommitController { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public ExternalServiceManager() 
        {
            PlatformRepositoryController = new GitlabController<Repository>();
            PlatformIssueController = new GithubController<Issue>();
            PlatformBranchController = new GithubController<Branch>();
            PlatformCommitController = new GithubController<Commit>();
        }

        #endregion

    }
}
