using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    public class GitlabController<T> : ControllerBase where T : ModelBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        public GitlabController()
        {
            Logger = LogManager.GetLogger(typeof(GitlabController<T>));
        }

        #endregion

        #region Methods

        #endregion
    }
}
