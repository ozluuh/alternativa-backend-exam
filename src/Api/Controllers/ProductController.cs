using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Attributes;
using Domain.Commands.Results;
using Domain.Entities;
using Domain.Mappings;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
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
            try
            {
                var response = await _repo.GetAllAsync();

                if (response.Count() == 0)
                {
                    return BadRequest();
                }

                var data = response.Select(entity => entity.ToCommandResult());

                return Ok(data);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCommandResult>> GetProductById([FromRoute] long id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var response = await _repo.GetByIdAsync(id);

                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ProductCommandResult>> StoreProduct([FromBody] StoreProductCommand storeProduct)
        {
            try
            {
                var response = await _repo.CreateAsync(storeProduct.ToDomain());
                await _repo.CommitAsync();

                return Ok(response.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
