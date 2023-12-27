using Model;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Classe permettant de définir les applications devant être utilisées pour chaque type de ressource.
    /// </summary>
    public class ExternalServiceManager
    {
        #region Properties

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des membres.
        /// </summary>
        public PlatformController<Member> PlatformMemberController { get; private set; }

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des répertoires.
        /// </summary>
        public PlatformController<Repository> PlatformRepositoryController { get; private set; }

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des issues.
        /// </summary>
        public PlatformController<Issue> PlatformIssueController { get; private set; }

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des branches.
        /// </summary>
        public PlatformController<Branch> PlatformBranchController { get; private set; }

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des commits.
        /// </summary>
        public PlatformController<Commit> PlatformCommitController { get; private set; }

        /// <summary>
        /// Obtient ou définit la plateforme à utiliser pour le contrôleur des fichiers.
        /// </summary>
        public PlatformController<Model.File> PlatformFileController { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ExternalServiceManager"/>.
        /// </summary>
        public ExternalServiceManager() 
        {
            PlatformRepositoryController = new GithubController<Repository>();
            PlatformIssueController = new GithubController<Issue>();
            PlatformBranchController = new GithubController<Branch>();
            PlatformCommitController = new GithubController<Commit>();
            PlatformFileController = new GithubController<Model.File>();
        }

        #endregion

    }
}
