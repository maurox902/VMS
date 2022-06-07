using System.Linq.Expressions;

namespace VMS.Repository
{
    public interface IRepository<T> : IDisposable
    {
        Task<int> Save(T model);
        Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = "");
        Task<IEnumerable<T>> GetPageableList(int? pageNumber, int pageSize, string includeProperties = "");
        Task<IEnumerable<T>> GetList(string includeProperties = "");
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<int> Delete(T model);
    }
}