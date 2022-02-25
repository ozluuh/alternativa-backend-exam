using System.Collections.Generic;
using System.Linq;
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
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
        }
    }
}
