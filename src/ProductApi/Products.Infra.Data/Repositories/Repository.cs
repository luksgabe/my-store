using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;

namespace Products.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected AppDbContext _context { get; }
        protected readonly IMongoCollection<TEntity> _mongoContext;
        public Repository(AppDbContext context, IMongoDatabaseSettings _mongoSettings)
        {
            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = false;

            var client = new MongoClient(_mongoSettings.ConnectionString);
            var mongoDb = client.GetDatabase(_mongoSettings.DatabaseName);
            _mongoContext = mongoDb.GetCollection<TEntity>(nameof(TEntity));
        }

        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _mongoContext.Find(p => true).ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
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
