using FluentValidation.Results;
using MediatR;
using Products.Application.Categories.Events;
using Products.Application.Configuration.Commands;
using Products.Application.Configuration.Messaging;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Productss.Domain.Entities;

namespace Products.Application.Categories.Commands
{
    public class CategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<RegisterCategoryCommand, ValidationResult>
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ValidationResult> Handle(RegisterCategoryCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var category = new Category(Guid.NewGuid(), message.Name);

            category.AddDomainEvent(new CategoryRegisterEvent(category.Id, category.Name));

            await _categoryRepository.AddAsync(category);

            return await Commit();
        }
    }
}
