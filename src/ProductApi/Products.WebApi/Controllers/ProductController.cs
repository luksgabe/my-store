using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products;
using Products.Application.Products.DTOs;
using System.Net;

namespace Products.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new List<string>() { "Calça", "Sapato", "Tênis" };
            return Ok(result);
        }

        /// <summary>
        /// Register customer.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductCommand request)
        {
            var product = await _mediator.Send(request);
            return Created(string.Empty, product);
        }
    }
}
