using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;
using Products.Infra.Data.Options;

namespace Products.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context, IMongoDatabaseSettings mongoSettings) : base(context, mongoSettings)
        {

        }
    }
}
