using FluentValidation.Results;
using Products.Application.Categories.Responses;
using Products.Application.Configuration.Queries;

namespace Products.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IQuery<IEnumerable<CategoryResponse>>
    {
    }
}
