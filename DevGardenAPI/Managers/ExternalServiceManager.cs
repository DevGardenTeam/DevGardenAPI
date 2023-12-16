using Model;

namespace DevGardenAPI.Managers
{
    public class ExternalServiceManager
    {
        #region Properties

        #region Member

        public GithubController<Repository> GithubRepositoryController { get; private set; }
        public GitlabController<Repository> GitlabRepositoryController { get; private set; }
        public GiteaController<Repository> GiteaRepositoryController { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public ExternalServiceManager() 
        {
            GithubRepositoryController = new GithubController<Repository>();
        }

        #endregion

    }
}
