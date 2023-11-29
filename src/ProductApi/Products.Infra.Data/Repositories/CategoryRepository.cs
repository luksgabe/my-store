using Products.Domain.Interfaces.Repositories;
using Products.Infra.Data.Context;
using Productss.Domain.Entities;

namespace Products.Infra.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
