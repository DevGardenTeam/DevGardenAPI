using Model;

namespace DevGardenAPI.Managers
{
    public class ExternalServiceManager
    {
        #region Properties

        #region Member

        public GithubController<Repository> GithubMemberController { get; private set; }
        public GitlabController<Repository> GitlabMemberController { get; private set; }
        public GiteaController<Repository> GiteaMemberController { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public ExternalServiceManager() 
        {
            GithubMemberController = new GithubController<Repository>();
        }

        #endregion

    }
}
