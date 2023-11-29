using Microsoft.EntityFrameworkCore;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data.Context;

namespace Products.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected AppDbContext _context { get; }

        public Repository(AppDbContext context)
        {
            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = false;
        }

        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await Task.Run(() => _dbSet.Update(obj));
        }

        public void Remove(long id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }       

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
