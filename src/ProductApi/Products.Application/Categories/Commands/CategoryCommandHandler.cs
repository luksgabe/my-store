using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Products.Application.Categories.Events;
using Products.Application.Configuration.Commands;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Products.Domain.Entities;


namespace Products.Application.Categories.Commands
{
    public class CategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<RegisterCategoryCommand, ValidationResult>,
        IRequestHandler<UpdateCategoryCommand, ValidationResult>,
        IRequestHandler<DeleteCategoryCommand, ValidationResult>
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ValidationResult> Handle(RegisterCategoryCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var categoryExists = (await _categoryRepository.GetCustomData(p => p.Name == message.Name)).FirstOrDefault();
            if(categoryExists != null)
            {
                AddError("This category already exists.");
                return ValidationResult;
            }

            var category = new Category(Guid.NewGuid(), message.Name);

            category.AddDomainEvent(new CategoryRegisterEvent(category.Id, category.Name));

            await _categoryRepository.AddAsync(category);

            return await Commit();
        }

        public async Task<ValidationResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var categoryExists = await _categoryRepository.GetByIdAsync(request.Id);
            if (categoryExists is null)
            {
                AddError("This category doesn't exist.");
                return ValidationResult;
            }

            var category = _mapper.Map<Category>(request);

            category.AddDomainEvent(new CategoryUpdateEvent(category.Id, category.Name));

            await _categoryRepository.UpdateAsync(category);
            return await Commit();
        }

        public async Task<ValidationResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
            {
                AddError("This category doesn't exist.");
                return ValidationResult;
            }

            category.AddDomainEvent(new CategoryDeleteEvent(category.Id, category.Name));
            await _categoryRepository.RemoveAsync(category);
            return await Commit();
        }
    }
}
