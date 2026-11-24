namespace StoreSystem.Data.Repositories;

/// <summary>Шаблонний (узагальнений) інтерфейс репозиторію.</summary>
public interface IRepository<T> where T : class
{
    IReadOnlyList<T> GetAll();
    T? GetById(int id);
    void Add(T item);
}
