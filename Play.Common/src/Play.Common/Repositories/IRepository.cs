using Play.Common.Service.Entities;
using System.Linq.Expressions;

namespace Play.Common.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}