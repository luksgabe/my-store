using MediatR;
using Products.Application.Categories.Responses;
using Products.Domain.Interfaces.Repositories;

namespace Products.Application.Categories.Queries
{
    public class CategoryQueryHandler : 
        IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponse>>,
        IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetAllAsync();
            return result.Select(s => new CategoryResponse { Id = s.Id, Name = s.Name }).ToList();
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetByIdAsync(request.Id);
            return new CategoryResponse { Id = result.Id, Name = result.Name };
        }
    }
}
