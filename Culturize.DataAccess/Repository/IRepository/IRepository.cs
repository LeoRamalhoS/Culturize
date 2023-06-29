using System.Linq.Expressions;

namespace Culturize.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T item);
        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
    }
}
