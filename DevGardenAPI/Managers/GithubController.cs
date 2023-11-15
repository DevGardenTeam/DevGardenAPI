using DevGardenAPI.GenericRepository;
using Model;

namespace DevGardenAPI.Managers
{
    public class GithubController<T> : DevGardenController<T> where T : ModelBase
    {
        #region Properties

        #endregion

        #region Constructor

        public GithubController(IRepository<T> repository) : base(repository)
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
