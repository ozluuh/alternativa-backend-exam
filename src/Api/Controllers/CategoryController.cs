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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCategory([FromRoute] long id)
        {
                if (id <= 0)
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
