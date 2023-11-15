using DevGardenAPI.GenericRepository;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace DevGardenAPI.Managers
{
    [ApiController]
    [Route("api/{version}/{controller}")]
    public abstract class DevGardenController<T> : ControllerBase where T: ModelBase
    {
        #region Fields

        private readonly IRepository<T> _repository;

        #endregion

        #region Properties

        protected ILog Logger { get; set; }

        #endregion

        #region Constructor

        public DevGardenController(IRepository<T> repository)
        {
            _repository = repository;
            Logger = LogManager.GetLogger(typeof(DevGardenController<>));
        }

        #endregion

        #region Methods

        [HttpGet("GetAll")]
        public async Task<IEnumerable<T>>? GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<T>? Get(string id)
        {
            return await _repository.GetById(id);
        }

        [HttpPost]
        public async Task<T> Post(T entity)
        {
            return await _repository.Add(entity);
        }

        [HttpPut]
        public async Task<T>? Put(T entity)
        {
            return await _repository.Update(entity);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        #endregion
    }
}
