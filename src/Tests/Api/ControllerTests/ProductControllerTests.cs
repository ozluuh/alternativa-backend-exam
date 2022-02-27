using System.Threading.Tasks;
using Api.Controllers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Api.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        [Trait("Api", "Controller")]
        public async Task GetProductList_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductList();

            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
