using DevGardenAPI.GenericRepository;
using Model;

namespace DevGardenAPI.Managers
{
    public class GiteaController<T> : DevGardenController where T : ModelBase
    {
        #region Properties

        #endregion

        #region Constructor

        public GiteaController(IRepository<T> repository) : base()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
