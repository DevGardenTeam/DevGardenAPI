using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    public class GiteaController<T> : ControllerBase where T : ModelBase
    {
        #region Fields

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        public GiteaController()
        {
            Logger = LogManager.GetLogger(typeof(GiteaController<T>));
        }

        #endregion

        #region Methods

        #endregion
    }
}
