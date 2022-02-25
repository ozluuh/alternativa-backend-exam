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
    public class CategoryControllerTests
    {
        [Fact]
        [Trait("Api", "Controller")]
        public async Task GetCategoryList_ReturnsOkObjectResult_WhenSuccessful() {

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Category>());
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryList();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
