using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Domain.Entities;
using Domain.Mappings;
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

        [Fact]
        [Trait("Product", "Controller")]
        public async Task StoreProduct_ReturnsOkObjectResult_WhenSuccessful()
        {
            var product = new Product()
            {
                Id = 1L,
                Name = "My Dress-Up Darling",
                Description = "Official Music Video",
                Brand = "Akase Akari",
                Value = 9,
                CategoryId = 1
            };

            var storeProduct = new StoreProductCommand()
            {
                Name = product.Name,
                Description = product.Description,
                Brand = product.Brand,
                Value = product.Value.GetValueOrDefault(),
                CategoryId = product.CategoryId
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Product>())).ReturnsAsync(product);
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.StoreProduct(storeProduct);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task StoreProduct_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var storeProduct = new StoreProductCommand()
            {
                Name = "My Dress-Up Darling",
                Description = "Official Music Video",
                Brand = "Akase Akari",
                Value = 9,
                CategoryId = 1
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<Product>())).Throws<Exception>();
            var controller = new ProductController(mockRepo.Object);

            var response = await controller.StoreProduct(storeProduct);
            var res = (StatusCodeResult)response.Result;

            Assert.IsType<StatusCodeResult>(response.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task UpdateProduct_ReturnsOkObjectResult_WhenSuccessfull()
        {
            var updateProductCommand = new UpdateProductCommand()
            {
                Id = 1L,
                Name = "My Dress-Up Darling",
                Description = "Official Music Video",
                Brand = "Akase Akari",
                Value = 9,
                CategoryId = 1
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(updateProductCommand.ToDomain());

            var controller = new ProductController(mockRepo.Object);

            var result = await controller.UpdateProduct(updateProductCommand);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task UpdateProduct_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var updateProduct = new UpdateProductCommand()
            {
                Id = 1L,
                Name = "My Dress-Up Darling",
                Description = "Official Music Video",
                Brand = "Akase Akari",
                Value = 9,
                CategoryId = 1
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .Throws<Exception>();

            var controller = new ProductController(mockRepo.Object);

            var response = await controller.UpdateProduct(updateProduct);
            var res = (StatusCodeResult)response.Result;

            Assert.IsType<StatusCodeResult>(response.Result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task UpdateProduct_ReturnsBadRequestResult_WhenIdIsNullOrLessOrEqualThanZero()
        {
            var updateProduct = new UpdateProductCommand()
            {
                Name = "My Dress-Up Darling",
                Description = "Official Music Video",
                Brand = "Akase Akari",
                Value = 9,
                CategoryId = 1
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo
                .Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .Throws<Exception>();

            var controller = new ProductController(mockRepo.Object);

            var response = await controller.UpdateProduct(updateProduct);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task DeleteProduct_ReturnsOkResult_WhenSuccessfull()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>()));

            var controller = new ProductController(mockRepo.Object);

            var result = await controller.DeleteProduct(1L);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task DeleteProduct_ReturnsBadRequestResult_WhenIdIsLessOrEqualThanZero()
        {
            var mockRepo = new Mock<IProductRepository>();

            var controller = new ProductController(mockRepo.Object);

            var result = await controller.DeleteProduct(-1L);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task DeleteProduct_ReturnsInternalServerErrorStatusCode_WhenAnyException()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>())).Throws<Exception>();
            var controller = new ProductController(mockRepo.Object);

            var result = await controller.DeleteProduct(999L);
            var res = (StatusCodeResult)result;

            Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(res.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [Fact]
        [Trait("Product", "Controller")]
        public async Task DeleteProduct_ReturnsBadRequestResult_WhenProductNotExists()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.NotExists(It.IsAny<long>())).ReturnsAsync(true);
            var controller = new ProductController(mockRepo.Object);

            var result = await controller.DeleteProduct(99999L);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
