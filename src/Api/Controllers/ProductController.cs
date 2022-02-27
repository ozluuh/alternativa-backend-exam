using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands.Results;
using Domain.Mappings;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCommandResult>>> GetProductList()
        {

            var response = await _repo.GetAllAsync();

            if (response.Count() == 0)
            {
                return BadRequest();
            }

            var data = response.Select(entity => entity.ToCommandResult());

            return Ok(data);
        }
    }
}
