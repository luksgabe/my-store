using Products.Domain.Entities;
using Productss.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task CreateCategory(Category category);
        Task UpdateCategory(Guid id, Category category);
        Task DeleteCategory(Category categoryForDeletion);
        Task DeleteCategory(Guid id);
        
    }
}
