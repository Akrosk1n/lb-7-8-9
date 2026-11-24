using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface IOrderRepository
{
    IReadOnlyList<Order> GetAll();
    Order? GetById(int id);
    int GetNextId();
    void Add(Order order);
}
