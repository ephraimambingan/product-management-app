using Moq;
using ProductManagementApp.Domain.Entities;
using ProductManagementApp.Domain.Repositories;
using ProductManagementApp.Services;

namespace ProductManagementApp.Tests;

public class ProductServiceTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private ProductService _productService;
    
    [SetUp]
    public void Setup()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _productService = new ProductService(_mockProductRepository.Object);
    }

    [Test]
    public async Task GetAllProducts_ReturnsNonEmptyList()
    {
        // arrange
        var mockProducts = new List<Product>
        {
            new() { Id = 1, Name = "Product 1", Price = 10.0m },
            new() { Id = 2, Name = "Product 2", Price = 15.0m },
        };

        _mockProductRepository
            .Setup(repo => repo.GetAll())
            .Returns(mockProducts.AsQueryable());

        // act
        var products = await _productService.GetAllProductsAsync();

        // assert
        Assert.IsNotNull(products);
        Assert.That(products.Count(), Is.EqualTo(2)); // assuming GetAllProductsAsync returns all products
    }
}