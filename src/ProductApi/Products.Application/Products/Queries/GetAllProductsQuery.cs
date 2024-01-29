using Products.Application.Configuration.Queries;
using Products.Application.Products.Responses;

namespace Products.Application.Products.Queries
{
    public class GetAllProductsQuery : ProductQuery, IQuery<IEnumerable<ProductResponse>>
    {
    }
}
