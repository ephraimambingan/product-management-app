using ProductManagementApp.Domain.Entities;
using ProductManagementApp.Domain.Repositories;
using ProductManagementApp.Services.DTOs;
using ProductManagementApp.Services.Extensions;

namespace ProductManagementApp.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _productRepository
            .GetAll()
            .Select(s => new ProductDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price
            })
            .ToListAsync();
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
            return null;
        
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task AddProductAsync(ProductDto product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };

        bool exists = _productRepository.Any(q => q.Name == product.Name);
        if (exists)
        {
            throw new ApplicationException("Product name already exists.");
        }
        
        _productRepository.Add(newProduct);
    }

    public async Task UpdateProductAsync(ProductDto product)
    {
        var existingProduct = _productRepository.GetById(product.Id);
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        _productRepository.Update(existingProduct);
    }

    public async Task DeleteProductAsync(int id)
    {
        _productRepository.Delete(id);
    }
}