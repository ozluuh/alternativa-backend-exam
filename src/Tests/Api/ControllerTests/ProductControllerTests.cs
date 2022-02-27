using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Api.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductList_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductList();

            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
