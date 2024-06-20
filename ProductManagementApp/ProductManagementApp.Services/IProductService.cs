using ProductManagementApp.Services.DTOs;

namespace ProductManagementApp.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task AddProductAsync(ProductDto product);
    Task UpdateProductAsync(ProductDto product);
    Task DeleteProductAsync(int id);
}