using AutoMapper;
using Moq;
using Xunit;

using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;

using Domain.Entities;

namespace Application.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _service = new ProductService(
                _repositoryMock.Object,
                _mapper);
        }

        // ===========================
        // Get All Products
        // ===========================

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProducts()
        {
            // Arrange

            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Laptop"
                },

                new Product
                {
                    Id = 2,
                    ProductName = "Mouse"
                }
            };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);

            // Act

            var result = await _service.GetAllProductsAsync(1, 5);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Laptop", result[0].ProductName);
        }

        // ===========================
        // Get Product By Id
        // ===========================

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            // Arrange

            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            // Act

            var result = await _service.GetProductByIdAsync(1);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Laptop", result.ProductName);
        }

        // ===========================
        // Create Product
        // ===========================

        [Fact]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            // Arrange

            var dto = new CreateProductDto
            {
                ProductName = "Keyboard"
            };

            _repositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act

            var result = await _service.CreateProductAsync(dto);

            // Assert

            Assert.NotNull(result);
            Assert.Equal("Keyboard", result.ProductName);

            _repositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Product>()),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        // ===========================
        // Update Product
        // ===========================

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnTrue()
        {
            // Arrange

            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

            var dto = new UpdateProductDto
            {
                ProductName = "Gaming Laptop"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _repositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act

            var result = await _service.UpdateProductAsync(1, dto);

            // Assert

            Assert.True(result);

            _repositoryMock.Verify(
                x => x.UpdateAsync(It.IsAny<Product>()),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        // ===========================
        // Delete Product
        // ===========================

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrue()
        {
            // Arrange

            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _repositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(x => x.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act

            var result = await _service.DeleteProductAsync(1);

            // Assert

            Assert.True(result);

            _repositoryMock.Verify(
                x => x.DeleteAsync(It.IsAny<Product>()),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}