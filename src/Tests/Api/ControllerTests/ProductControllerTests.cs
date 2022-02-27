using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
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
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>(){
                new Product()
            });
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductList();

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductList_ReturnsBadRequestResult_WhenNoData()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductList();

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductList_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllAsync()).Throws<Exception>();
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductList();

            var res = (StatusCodeResult)response.Result;

            Assert.IsType<StatusCodeResult>(response.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductById_ReturnsOkObjectResult_WhenSuccessful()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(1L)).ReturnsAsync(new Product());
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductById(1L);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductById_ReturnsBadRequestResult_WhenIdLessOrEqualThanZero()
        {
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductById(-1L);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductById_ReturnsBadRequestResult_WhenNoDataFound()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(99L)).ReturnsAsync(() => null);
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductById(99L);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task GetProductById_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<long>())).Throws<Exception>();
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.GetProductById(999L);
            var res = (StatusCodeResult)response.Result;

            Assert.IsType<StatusCodeResult>(response.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }
    }
}
