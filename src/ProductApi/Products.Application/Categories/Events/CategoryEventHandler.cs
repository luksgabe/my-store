using MediatR;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Productss.Domain.Entities;

namespace Products.Application.Categories.Events
{
    public class CategoryEventHandler : IEventHandler<CategoryRegisterEvent>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryEventHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CategoryRegisterEvent notification, CancellationToken cancellationToken)
        {
            await _categoryRepository.CreateCategory(new Category(notification.Id, notification.Name));
        }
    }
}
