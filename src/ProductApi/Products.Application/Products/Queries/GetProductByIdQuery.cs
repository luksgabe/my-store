using Products.Application.Configuration.Queries;
using Products.Application.Products.Responses;

namespace Products.Application.Products.Queries
{
    public class GetProductByIdQuery : ProductQuery, IQuery<ProductResponse>
    {
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
