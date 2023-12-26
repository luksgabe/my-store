using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Products.Application.Categories.Events;
using Products.Application.Configuration.Commands;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;
using Productss.Domain.Entities;


namespace Products.Application.Categories.Commands
{
    public class CategoryCommandHandler : CommandHandlerBase,
        IRequestHandler<RegisterCategoryCommand, ValidationResult>
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

            var category = _mapper.Map<Category>(message);

            category.AddDomainEvent(new CategoryRegisterEvent(category.Id, category.Name));

            await _categoryRepository.AddAsync(category);

            return await Commit();
        }
    }
}
