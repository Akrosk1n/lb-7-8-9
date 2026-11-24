using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataStorage _storage;

    public ProductRepository(DataStorage storage) => _storage = storage;

    public IReadOnlyList<Product> GetAll() => _storage.Products;

    public Product? GetById(int id) => _storage.Products.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<Product> FindByName(string text) =>
        _storage.Products
            .Where(p => p.Name.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();

    public IReadOnlyList<Product> FindByCategory(int categoryId) =>
        _storage.Products.Where(p => p.Category.Id == categoryId).ToList();

    public int GetNextId() => _storage.Products.Count == 0 ? 1 : _storage.Products.Max(p => p.Id) + 1;

    public void Add(Product item) => _storage.Products.Add(item);
}
