using AutoMapper;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.SeedWork;

namespace Products.Application.Products.Events
{
    public class ProductEventHandler : 
        IEventHandler<ProductRegisterEvent>,
        IEventHandler<ProductUpdateEvent>,
        IEventHandler<ProductDeleteEvent>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductEventHandler(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(ProductRegisterEvent @event, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(@event);
            product.SetCategory(await _categoryRepository.GetByIdAsync(product.IdCategory));

            await _productRepository.CreateNoSql(product);
        }

        public async Task Handle(ProductUpdateEvent @event, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(@event);
            product.SetCategory(await _categoryRepository.GetByIdAsync(product.IdCategory));
            product.Update();
            await _productRepository.UpdateNoSql(product.Id, product);
        }

        public async Task Handle(ProductDeleteEvent @event, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(@event.Id);
            product.Delete();
            await _productRepository.DeleteNoSql(@event.Id, product);
        }
    }
}
