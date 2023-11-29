using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Categories.Commands;
using Products.Application.Categories.DTOs;
using Products.Application.Categories.Requests;
using Products.Application.Configuration;
using System.Net;

namespace Products.WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IMediatorHandler _mediator;

        public CategoryController(IMediatorHandler mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Register category.
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterCategoryRequest request)
        {
            var category = await _mediator.SendCommand(new RegisterCategoryCommand(request.Name));
            return Created(string.Empty, category);
        }
    }
}
