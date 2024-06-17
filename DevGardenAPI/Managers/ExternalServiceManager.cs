using DatabaseEf;
using Model;

namespace DevGardenAPI.Managers
{
    /// <summary>
    /// Classe permettant de définir les applications devant être utilisées pour chaque type de ressource.
    /// </summary>
    public class ExternalServiceManager
    {
        /// <summary>
        /// The github controller.
        /// </summary>
        public GithubController GithubController { get; set; }

        /// <summary>
        /// The gitlab controller.
        /// </summary>
        public GitlabController GitlabController { get; set; }

        /// <summary>
        /// The gitea controller.
        /// </summary>
        public GiteaController GiteaController { get; set; }

        /// <summary>
        /// The platform controller accessor.
        /// </summary>
        Dictionary<string, PlatformController> PlatformAccessor { get; set; }


        public ExternalServiceManager(TokenService tokenService) 
        {
            GithubController = new GithubController(tokenService);
            GitlabController = new GitlabController(tokenService);
            GiteaController = new GiteaController(tokenService);

            this.PlatformAccessor = new Dictionary<string, PlatformController>()
            {
                { "github", this.GithubController },
                { "gitlab", this.GitlabController },
                { "gitea", this.GiteaController },
            } ;
        }

        public PlatformController GetController(string platform)
        {
            if (PlatformAccessor.TryGetValue(platform, out PlatformController? value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException("Unknown platform", nameof(platform));
            }
        }
    }
}
