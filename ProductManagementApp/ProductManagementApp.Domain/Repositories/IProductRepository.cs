using ProductManagementApp.Domain.Entities;

namespace ProductManagementApp.Domain.Repositories;

public interface IProductRepository
{
    bool Any(Func<Product, bool> predicate);
    IQueryable<Product> GetAll();
    Product? GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}