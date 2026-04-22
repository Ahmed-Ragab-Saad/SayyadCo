using SayyadCo.Domain.Common;
using System.Linq.Expressions;

namespace SayyadCo.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<PagedResult<T>> GetAllAsync(QueryParameters parameters);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> ExistingAsync(string id);
    }
}
