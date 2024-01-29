using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Products.Application.Configuration.Commands;
using Products.Application.Configuration.Messaging;
using Products.Application.Products.Events;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Products.Commands
{
    public class ProductCommandHandler : CommandHandlerBase,
        IRequestHandler<RegisterProductCommand, ValidationResult>,
        IRequestHandler<UpdateProductCommand, ValidationResult>,
        IRequestHandler<DeleteProductCommand, ValidationResult>
    {

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public ProductCommandHandler(IMapper mapper,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ValidationResult> Handle(RegisterProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;
            var category = await _categoryRepository.GetByIdAsync(message.IdCategory);
            if(category is null)
            {
                AddError("Category doesn't exist.");
                return ValidationResult;
            }

            var product = new Product(Guid.NewGuid(),
                        message.Name,
                        message.Description,
                        message.Color,
                        message.Size,
                        message.IdCategory,
                        message.Genre);

            var productEvent = _mapper.Map<ProductRegisterEvent>(product);
            product.AddDomainEvent(productEvent);

            await _productRepository.AddAsync(product);

            return await Commit();
        }

        public async Task<ValidationResult> Handle(UpdateProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var product = await _productRepository.GetByIdAsync(message.Id);
            if (product is null)
            {
                AddError("Product doesn't exist.");
                return ValidationResult;
            }

            var category = await _categoryRepository.GetByIdAsync(message.IdCategory);
            if (category is null)
            {
                AddError("Category doesn't exist.");
                return ValidationResult;
            }

            product = new Product(message.Id,
                        message.Name,
                        message.Description,
                        message.Color,
                        message.Size,
                        message.IdCategory,
                        message.Genre);

            var productEvent = _mapper.Map<ProductUpdateEvent>(product);
            product.AddDomainEvent(productEvent);

            await _productRepository.UpdateAsync(product);

            return await Commit();
        }

        public async Task<ValidationResult> Handle(DeleteProductCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var product = await _productRepository.GetByIdAsync(message.Id);
            if (product is null)
            {
                AddError("Product doesn't exist.");
                return ValidationResult;
            }

            var productEvent = new ProductDeleteEvent(message.Id);
            product.AddDomainEvent(productEvent);

            await _productRepository.RemoveAsync(product);
            return await Commit();
        }
    }
}
