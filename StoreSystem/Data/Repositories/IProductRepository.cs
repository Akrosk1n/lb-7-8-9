using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface IProductRepository
{
    IReadOnlyList<Product> GetAll();
    Product? GetById(int id);
    IReadOnlyList<Product> FindByName(string text);
    IReadOnlyList<Product> FindByCategory(int categoryId);
    int GetNextId();
    void Add(Product product);
}
