using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataStorage _storage;

    public CategoryRepository(DataStorage storage) => _storage = storage;

    public IReadOnlyList<Category> GetAll() => _storage.Categories;

    public Category? GetById(int id) => _storage.Categories.FirstOrDefault(c => c.Id == id);
}
