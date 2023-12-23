using Microsoft.AspNetCore.Mvc;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Responses;
using Products.Application.Categories.Requests;
using Products.Application.Configuration;
using System.Net;
using Products.Application.Categories.Queries;

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
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterCategoryRequest request)
        {
            var category = await _mediator.SendCommand(new RegisterCategoryCommand(request.Name));
            return Created(string.Empty, category);
        }

        /// <summary>
        /// Get category.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.SendQuery(new GetAllCategoriesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get category.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.SendQuery(new GetCategoryByIdQuery(id));
            if (result is null)
                return NotFound();
            
            return Ok(result);
        }
    }
}
