using Products.Domain.Interfaces.SeedWork;
using Products.Domain.Entities;
using System.Linq.Expressions;

namespace Products.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetCustomData(Expression<Func<TEntity, bool>> expression);
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(TEntity entity);
        Task CreateNoSql(TEntity entity);
        Task UpdateNoSql(Guid id, TEntity entity);
        Task DeleteNoSql(Guid id);
        Task DeleteNoSql(Guid id, TEntity entityForDeletion);
    }
}
