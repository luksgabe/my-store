using AutoMapper;
using MediatR;
using Products.Application.Products.Responses;
using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Products.Queries
{
    public class ProductQueryHandler : 
        IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>,
        IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ProductResponse>(product);
        }
    }
}