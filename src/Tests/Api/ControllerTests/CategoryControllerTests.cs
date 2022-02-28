using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Commands.Inputs;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Api.ControllerTests
{
    public class CategoryControllerTests
    {
        [Fact]
        [Trait("Category", "Controller")]
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
        [Trait("Category", "Controller")]
        public async Task GetCategoryList_ReturnsBadRequestResult_WhenNoData()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Category>());
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryList();

            Assert.IsType<BadRequestResult>(result.Result);
        }


        [Fact]
        [Trait("Category", "Controller")]
        public async Task GetCategoryList_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Throws<Exception>();
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryList();

            var res = (StatusCodeResult)result.Result;

            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task GetCategoryById_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1L)).ReturnsAsync(new Category());
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryById(1L);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task GetCategoryById_ReturnsBadRequestResult_WhenNoData()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(-1L)).ReturnsAsync(() => null);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryById(-1L);

            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task GetCategoryById_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(99999L)).Throws<Exception>();
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.GetCategoryById(99999L);

            var res = (StatusCodeResult)result.Result;

            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Category", "Controller")]
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
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Category>())).ReturnsAsync(category);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.StoreCategory(storeCategoryCommand);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task StoreCategory_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var storeCategoryCommand = new StoreCategoryCommand()
            {
                Description = "Fukashigi no Carte",
                Name = "Seishun Buta Yarou wa Bunny Girl Senpai no Yume wo Minai"
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Category>())).Throws<Exception>();
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.StoreCategory(storeCategoryCommand);

            var res = (StatusCodeResult)result.Result;

            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Category", "Controller")]
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
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).ReturnsAsync(category);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.UpdateCategory(updateCategoryCommand);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task UpdateCategory_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var updateCategoryCommand = new UpdateCategoryCommand()
            {
                Id = 1L,
                Description = "Fukashigi no Carte",
                Name = "Seishun Buta Yarou wa Bunny Girl Senpai no Yume wo Minai"
            };

            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Category>())).Throws<Exception>();
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.UpdateCategory(updateCategoryCommand);

            var res = (StatusCodeResult)result.Result;

            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task DeleteCategory_ReturnsOkResult_WhenSuccessfull()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.DeleteAsync(1L));
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.RemoveCategory(1L);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task DeleteCategory_ReturnsBadRequestResult_WhenCategoryHasDependentsOrNotExists()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.NotExistsOrHasDependents(1L)).ReturnsAsync(true);
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.RemoveCategory(1L);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        [Trait("Category", "Controller")]
        public async Task DeleteCategory_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>())).Throws<Exception>();
            var controller = new CategoryController(mockRepo.Object);

            var result = await controller.RemoveCategory(999L);
            var res = (StatusCodeResult)result;

            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }
    }
}
