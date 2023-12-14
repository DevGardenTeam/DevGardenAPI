using DevGardenAPI.GenericRepository;
using Model;

namespace DevGardenAPI.Managers
{
    public class GitlabController<T> : DevGardenController where T : ModelBase
    {
        #region Properties

        #endregion

        #region Constructor

        public GitlabController(IRepository<T> repository) : base()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
