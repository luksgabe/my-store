using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;
using Products.Domain.Entities;

namespace Products.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(AppDbContext context, IMongoDatabaseSettings mongoSettings) : base(context, mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);

            _productCollection = database.GetCollection<Product>(nameof(Product));
            base.MongoDB(_productCollection);
        }
    }
}
