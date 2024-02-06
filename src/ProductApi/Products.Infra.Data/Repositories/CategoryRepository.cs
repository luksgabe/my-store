using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;
using Products.Domain.Entities;
namespace Products.Infra.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(AppDbContext context, IMongoDatabaseSettings mongoSettings) : base(context, mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(nameof(Category));
            base.MongoDB(_categoryCollection);
        }
        public async Task CreateCategory(Category category) => await _categoryCollection.InsertOneAsync(category);

        public async Task UpdateCategory(Guid id, Category category) => await _categoryCollection.ReplaceOneAsync(c => c.Id == id, category);

        public async Task DeleteCategory(Category categoryForDeletion) => _categoryCollection.DeleteOne(category => category.Id == categoryForDeletion.Id);

        public async Task DeleteCategory(Guid id) => _categoryCollection.DeleteOne(category => category.Id == id);       
    }
}
