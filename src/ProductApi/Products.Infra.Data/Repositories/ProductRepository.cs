using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;

namespace Products.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
    }
}
