using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Commands.Inputs;
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
        public async Task GetCategoryList_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Category>() {
                new Category()
            });
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryList();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Api", "Controller")]
        public async Task GetCategoryList_ReturnsBadRequestResult_WhenNoData()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Category>());
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryList();

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        [Trait("Api", "Controller")]
        public async Task GetCategoryById_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1L)).ReturnsAsync(new Category());
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryById(1L);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Api", "Controller")]
        public async Task GetCategoryById_ReturnsBadRequestResult_WhenNoData()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(-1L)).ReturnsAsync(() => null);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryById(-1L);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        [Trait("Api", "Controller")]
        public async Task StoreCategory_ReturnsOkObjectResult_WhenSuccessfull()
        {
            var category = new Category()
            {
                Id = 1L,
                Description = "Fukashigi no Carte",
                Name = "Seishun Buta Yarou wa Bunny Girl Senpai no Yume wo Minai"
            };

            var storeCategoryCommand = new StoreCategoryCommand()
            {
                Name = category.Name,
                Description = category.Description
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(category)).ReturnsAsync(category);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.StoreCategory(storeCategoryCommand);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Api", "Controller")]
        public async Task UpdateCategory_ReturnsOkObjectResult_WhenSuccessfull()
        {
            var category = new Category()
            {
                Id = 1L,
                Description = "Fukashigi no Carte",
                Name = "Seishun Buta Yarou wa Bunny Girl Senpai no Yume wo Minai"
            };

            var updateCategoryCommand = new UpdateCategoryCommand()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.UpdateAsync(category)).ReturnsAsync(category);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.UpdateCategory(updateCategoryCommand);

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
