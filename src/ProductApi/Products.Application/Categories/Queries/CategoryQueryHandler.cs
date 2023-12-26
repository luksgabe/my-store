using AutoMapper;
using MediatR;
using Products.Application.Categories.Responses;
using Products.Domain.Interfaces.Repositories;
using Productss.Domain.Entities;

namespace Products.Application.Categories.Queries
{
    public class CategoryQueryHandler : 
        IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>,
        IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}
