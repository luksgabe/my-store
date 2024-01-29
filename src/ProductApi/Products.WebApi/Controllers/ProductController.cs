using Microsoft.AspNetCore.Mvc;
using Products.Application.Configuration;
using Products.Application.Products.Commands;
using Products.Application.Products.Queries;
using Products.Application.Products.Requests;
using Products.Application.Products.Responses;
using System.Net;

namespace Products.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ApiController
    {
        private readonly IMediatorHandler _mediator;

        public ProductsController(IMediatorHandler mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Return a product by id.
        /// <paramref name="id"/>
        /// <return>An product</return>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.SendQuery(new GetProductByIdQuery(id));
            return GetCustomResponse(result);
        }

        /// <summary>
        /// Return a product list.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.SendQuery(new GetAllProductsQuery());
            return GetCustomResponse(result);
        }

        /// <summary>
        /// Register product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest request)
        {
            return !ModelState.IsValid ? 
                CustomResponse(ModelState) 
                : CustomResponse(await _mediator.SendCommand(
                    new RegisterProductCommand(request.Name, 
                        request.Description,
                        request.Color, 
                        request.Size, 
                        request.Genre, 
                        request.IdCategory)));
        }

        /// <summary>
        /// Update product.
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            return !ModelState.IsValid ?
                CustomResponse(ModelState)
                : CustomResponse(await _mediator.SendCommand(
                    new UpdateProductCommand(
                        request.Id,
                        request.Name,
                        request.Description,
                        request.Color,
                        request.Size,
                        request.Genre,
                        request.IdCategory)));
        }


        /// <summary>
        /// Update product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _mediator.SendCommand(new DeleteProductCommand(id)));
        }

    }
}
