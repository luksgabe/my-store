using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;
using Products.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Products.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected AppDbContext _context { get; }
        protected IMongoCollection<TEntity> _mongoContext;
        public Repository(AppDbContext context, IMongoDatabaseSettings _mongoSettings)
        {
            _context = context;
            _context.ChangeTracker.LazyLoadingEnabled = false;

            var client = new MongoClient(_mongoSettings.ConnectionString);
            var mongoDb = client.GetDatabase(_mongoSettings.DatabaseName);
        }

        protected void MongoDB(IMongoCollection<TEntity> mongoCollection)
        {
            _mongoContext = mongoCollection;
        }

        private DbSet<TEntity> _dbSet => _context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            DateTime? deletedAt = null;
            var filter = Builders<TEntity>.Filter.Eq("DeletedAt", deletedAt);

            var result = await _mongoContext.Find(filter).ToListAsync();
            return result;
        }

        public virtual async Task<IEnumerable<TEntity>> GetCustomData(Expression<Func<TEntity, bool>> expression)
        {
            DateTime? deletedAt = null;
            var filter = Builders<TEntity>.Filter.Eq("DeletedAt", deletedAt);
            filter = filter & Builders<TEntity>.Filter.Where(expression);
            return await _mongoContext.Find(filter).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            DateTime? deletedAt = null;
            var filter = Builders<TEntity>.Filter.Eq("DeletedAt", deletedAt);
            filter = filter & Builders<TEntity>.Filter.Eq("Id", id);
            return await _mongoContext.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            await Task.Run(() => _dbSet.Update(obj));
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await Task.FromResult(_dbSet.Remove(entity));
        }       

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task CreateNoSql(TEntity entity)
            => await _mongoContext.InsertOneAsync(entity);

        public virtual async Task UpdateNoSql(Guid id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            await _mongoContext.ReplaceOneAsync(filter, entity);
        }

        public virtual async Task DeleteNoSql(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            _mongoContext.DeleteOne(filter);
        }

        public virtual async Task DeleteNoSql(Guid id, TEntity entityForDeletion)
        {
            await this.UpdateNoSql(id, entityForDeletion);
        }

        
    }
}
