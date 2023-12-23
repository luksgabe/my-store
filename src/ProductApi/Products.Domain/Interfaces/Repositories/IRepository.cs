using Products.Domain.Interfaces.SeedWork;

namespace Products.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity obj);
        void Remove(long id);
    }
}
