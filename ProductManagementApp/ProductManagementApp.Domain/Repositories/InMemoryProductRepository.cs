using ProductManagementApp.Domain.Entities;

namespace ProductManagementApp.Domain.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public bool Any(Func<Product, bool> predicate)
    {
        return _products.Where(predicate).Any();
    }

    public IQueryable<Product> GetAll()
    {
        return _products?.AsQueryable();
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
        }
    }

    public void Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}