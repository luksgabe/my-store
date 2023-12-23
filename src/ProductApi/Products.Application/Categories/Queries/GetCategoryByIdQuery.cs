using Products.Application.Categories.Responses;
using Products.Application.Configuration.Queries;

namespace Products.Application.Categories.Queries
{
    public class GetCategoryByIdQuery : IQuery<CategoryResponse>
    {
        public Guid Id { get; private set; }

        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
