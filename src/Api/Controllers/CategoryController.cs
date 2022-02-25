using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
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

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById([FromRoute] long id)
        {
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
            {
                return BadRequest(data);
            }

            return Ok(data);
        }
    }
}
