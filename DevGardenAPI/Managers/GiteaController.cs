using DevGardenAPI.GenericRepository;
using Model;

namespace DevGardenAPI.Managers
{
    public class GiteaController<T> : DevGardenController<T> where T : ModelBase
    {
        #region Properties

        #endregion

        #region Constructor

        public GiteaController(IRepository<T> repository) : base(repository)
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
