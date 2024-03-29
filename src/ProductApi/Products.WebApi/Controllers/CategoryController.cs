﻿using Microsoft.AspNetCore.Mvc;
using Products.Application.Categories.Commands;
using Products.Application.Categories.Responses;
using Products.Application.Categories.Requests;
using Products.Application.Configuration;
using System.Net;
using Products.Application.Categories.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Products.WebApi.Controllers
{
    [Authorize]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ApiController
    {
        private readonly IMediatorHandler _mediator;


        public CategoryController(IMediatorHandler mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get category.
        /// <paramref name="id"/>
        /// /// <return>An specifically category</return>
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.SendQuery(new GetCategoryByIdQuery(id));
            if (result is null)
                return NotFound();

            return GetCustomResponse(result);
        }

        /// <summary>
        /// Get category.
        /// <return>List of categories</return>
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.SendQuery(new GetAllCategoriesQuery());
            return GetCustomResponse(result);
        }




        /// <summary>
        /// Register category.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> RegisterCategory([FromBody] RegisterCategoryRequest request)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _mediator.SendCommand(new RegisterCategoryCommand(request.Name)));
        }

        /// <summary>
        /// Update category.
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _mediator.SendCommand(new UpdateCategoryCommand(request.Id, request.Name)));
        }

        /// <summary>
        /// Delete category.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadGateway)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _mediator.SendCommand(new DeleteCategoryCommand(id)));
        }
    }
}
