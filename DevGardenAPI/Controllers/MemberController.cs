using DatabaseEf;
using DevGardenAPI.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace DevGardenAPI.Controllers
{
    /// <summary>
    /// Contrôleur de l'application DevGarden pour la partie Member.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        /// <summary>
        /// Obtient ou définit le gestionnaire de log.
        /// </summary>
        protected ILog Logger { get; set; }

        /// <summary>
        /// Obtient le manager du service utilisé.
        /// </summary>
        public ExternalServiceManager ExternalServiceManager { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="MemberController"/>.
        /// </summary>
        public MemberController(TokenService tokenService)
        {
            Logger = LogManager.GetLogger(typeof(MemberController));
            ExternalServiceManager = new ExternalServiceManager(tokenService);
        }
    }
}
