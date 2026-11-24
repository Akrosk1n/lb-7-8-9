using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IReadOnlyList<Product> FindByName(string text);
    IReadOnlyList<Product> FindByCategory(int categoryId);
    int GetNextId();
}
