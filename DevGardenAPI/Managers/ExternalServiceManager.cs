using Model;

namespace DevGardenAPI.Managers
{
    public class ExternalServiceManager
    {
        #region Properties

        #region Member

        public GithubController<Member> GithubMemberController { get; private set; }
        public GitlabController<Member> GitlabMemberController { get; private set; }
        public GiteaController<Member> GiteaMemberController { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public ExternalServiceManager() 
        {
            GithubMemberController = new GithubController<Member>();
        }

        #endregion

    }
}
