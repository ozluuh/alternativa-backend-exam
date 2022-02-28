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
    [Produces("application/json")]
    // NOTE: Defined `object` type to prevents Swagger shows response `example value`
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository productRepository)
        {
            _repo = productRepository;
        }

        /// <summary>Obtain a product list.</summary>
        /// <response code="400">In this case, if the list is equal to 0</response>
        /// <response code="500">If any exception occurs</response>
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

        /// <summary>Obtain a product data by {id}</summary>
        /// <param name="id"></param>
        /// <response code="400">if {id} is less than or equal to 0 or is null</response>
        /// <response code="500">If any exception occurs</response>
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

        /// <summary>Store product data</summary>
        /// <remark>
        /// Sample request:
        ///
        ///     POST /api/product
        ///     {
        ///        "name": "product",
        ///        "description": "asfafasfsafas",
        ///        "value": 0,
        ///        "brand": "Brand",
        ///        "CategoryId": 1
        ///     }
        ///</remark>
        /// <param name="StoreProductCommand"></param>
        /// <response code="400">if property validation does not pass</response>
        /// <response code="500">If any exception occurs</response>
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

        /// <summary>Update product data</summary>
        /// <remark>
        /// Sample request:
        ///
        ///     POST /api/product
        ///     {
        ///        "id": 1,
        ///        "name": "product",
        ///        "description": "asfafasfsafas",
        ///        "value": 0,
        ///        "brand": "Brand",
        ///        "CategoryId": 1
        ///     }
        ///</remark>
        /// <param name="UpdateProductCommand"></param>
        /// <response code="400">if property validation does not pass</response>
        /// <response code="500">If any exception occurs</response>
        [HttpPut]
        [ValidateModel]
        public async Task<ActionResult<ProductCommandResult>> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            // NOTE: Validate if `id` not filled or less/equal than 0
            // COMMENT: Implementation beyond API documentation, since this causes new entry in database
            // TIP: Validation must be change to `Required` or other annotation in Command Input
            if (updateProductCommand.Id <= 0)
            {
                return BadRequest(new { id = "Required/Must be filled" });
            }

            try
            {
                var product = await _repo.UpdateAsync(updateProductCommand.ToDomain());
                await _repo.CommitAsync();

                return Ok(product.ToCommandResult());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>Remove product data by {id}</summary>
        /// <param name="id"></param>
        /// <response code="400">if {id} is less than or equal to 0 or {product} not exists</response>
        /// <response code="500">If any exception occurs</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] long id)
        {
            // NOTE: Implementation beyond API documentation, to cover all cases
            if (id <= 0)
            {
                return BadRequest();
            }

            bool NotExists = await _repo.NotExists(id);

            if (NotExists)
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
