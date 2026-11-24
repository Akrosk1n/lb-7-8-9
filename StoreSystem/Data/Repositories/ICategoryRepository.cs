using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface ICategoryRepository
{
    IReadOnlyList<Category> GetAll();
    Category? GetById(int id);
}
