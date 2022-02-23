using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private readonly ApiDbContext _context;

        public CategoryController(ILogger<CategoryController> logger, ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> Index()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
