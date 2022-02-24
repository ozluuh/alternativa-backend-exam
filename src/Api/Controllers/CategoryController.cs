using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("/api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private readonly ICategoryRepository _repo;

        public CategoryController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<CategoryController>>();
            _repo = serviceProvider.GetService<ICategoryRepository>();
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            return await _repo.GetAllAsync();
        }

    }
}
