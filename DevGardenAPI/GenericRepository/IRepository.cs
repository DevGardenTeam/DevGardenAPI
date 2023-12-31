using Model;

namespace DevGardenAPI.GenericRepository
{
    public interface IRepository<T> where T : ModelBase
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(string entityId);
        Task<IEnumerable<T>> GetAll();
        Task<T>? GetById(string entityId);
    }
}
