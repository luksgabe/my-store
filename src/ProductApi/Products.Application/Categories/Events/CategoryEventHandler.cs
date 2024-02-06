using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Domain.Entities;

namespace Products.Application.Categories.Events
{
    public class CategoryEventHandler : 
        IEventHandler<CategoryRegisterEvent>,
        IEventHandler<CategoryUpdateEvent>,
        IEventHandler<CategoryDeleteEvent>
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

        public async Task Handle(CategoryUpdateEvent notification, CancellationToken cancellationToken)
        {
            var category = new Category(notification.Id, notification.Name);
            category.Update();
            await _categoryRepository.UpdateNoSql(category.Id, category);
        }

        public async Task Handle(CategoryDeleteEvent notification, CancellationToken cancellationToken)
        {
            var category = new Category(notification.Id, notification.Name);
            category.Delete();
            await _categoryRepository.DeleteNoSql(category.Id, category);
        }
    }
}
