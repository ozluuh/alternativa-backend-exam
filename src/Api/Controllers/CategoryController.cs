using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Attributes;
using Domain.Commands.Inputs;
using Domain.Commands.Results;
using Domain.Mappings;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/category")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    // NOTE: Defined `object` type to prevents Swagger shows response `example value`
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Obtain a category list.</summary>
        /// <response code="400">In this case, if the list is equal to 0</response>
        /// <response code="500">If any exception occurs</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryCommandResult>>> GetCategoryList()
        {
            try
            {
                var response = await _repo.GetAllAsync();

                // NOTE: Only demo for test purposes
                // TIP: If empty list, return 200 code
                if (response.Count() == 0)
                {
                    return BadRequest();
                }

                var data = response.Select(e => e.ToCommandResult());

                return Ok(data);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>Obtain a category data by {id}</summary>
        /// <param name="id"></param>
        /// <response code="400">if {id} is less than or equal to 0 or is null</response>
        /// <response code="500">If any exception occurs</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryCommandResult>> GetCategoryById([FromRoute] long id)
        {
            // COMMENT: Prevents unnecessary database query
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var data = await _repo.GetByIdAsync(id);

                if (data == null)
                {
                    return BadRequest();
                }

                return Ok(data.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>Store category data</summary>
        /// <remark>
        /// Sample request:
        ///
        ///     POST /api/category
        ///     {
        ///        "name": "categoria",
        ///        "description": "asfafasfsafas"
        ///     }
        ///</remark>
        /// <param name="StoreCategoryCommand"></param>
        /// <response code="400">if property validation does not pass</response>
        /// <response code="500">If any exception occurs</response>
        // COMMENT: ValidateModel check and return BadRequest if model is invalid
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CategoryCommandResult>> StoreCategory([FromBody] StoreCategoryCommand category)
        {
            try
            {
                var response = await _repo.CreateAsync(category.ToDomain());
                await _repo.CommitAsync();

                // NOTE: Only for testing purposes
                // TIP: When creating register, return `201 CREATED` over `200 OK`
                return Ok(response.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>Updates category data</summary>
        /// <remark>
        /// Sample request:
        ///
        ///     PUT /api/category
        ///     {
        ///        "id": 1,
        ///        "name": "categoria",
        ///        "description": "asfafasfsafas"
        ///     }
        ///</remark>
        /// <param name="UpdateCategoryCommand"></param>
        /// <response code="400">if property validation does not pass</response>
        /// <response code="500">If any exception occurs</response>
        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<CategoryCommandResult>> UpdateCategory([FromBody] UpdateCategoryCommand category)
        {
            try
            {
                var response = await _repo.UpdateAsync(category.ToDomain());
                await _repo.CommitAsync();

                return Ok(response.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>Remove category data by {id}</summary>
        /// <param name="long"></param>
        /// <response code="400">if {id} is less than or equal to 0, {category} not exists or has {products} dependants</response>
        /// <response code="500">If any exception occurs</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCategory([FromRoute] long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var NotExistsOrHasDependents = await _repo.NotExistsOrHasDependents(id);
            // NOTE: Validates if category not exists or have any related products
            // instead of waiting to throw an exception trying to delete
            if (NotExistsOrHasDependents)
            {
                return BadRequest();
            }

            try
            {
                await _repo.DeleteAsync(id);
                await _repo.CommitAsync();

                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
