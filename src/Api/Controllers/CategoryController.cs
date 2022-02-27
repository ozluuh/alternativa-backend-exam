using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Attributes;
using Domain.Commands.Inputs;
using Domain.Commands.Results;
using Domain.Entities;
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
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryList()
        {
            var data = await _repo.GetAllAsync();

            // NOTE: Only demo for test purposes
            // TIP: If empty list, return 200 code
            if (data.Count() == 0)
            {
                return BadRequest();
            }

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById([FromRoute] long id)
        {
            try{
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
            }
            catch(Exception){
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CategoryCommandResult>> StoreCategory([FromBody] StoreCategoryCommand category)
        {
            var response = await _repo.CreateAsync(category);
            await _repo.CommitAsync();

            // NOTE: Only for testing purposes
            // TIP: When creating register, return `201 CREATED` over `200 OK`
            return Ok(response);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<CategoryCommandResult>> UpdateCategory([FromBody] UpdateCategoryCommand category)
        {
            try
            {
                var response = await _repo.UpdateAsync(category);
                await _repo.CommitAsync();

                return Ok(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveCategory([FromRoute] long id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }

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
