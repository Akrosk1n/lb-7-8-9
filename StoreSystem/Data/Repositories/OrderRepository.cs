using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataStorage _storage;

    public OrderRepository(DataStorage storage) => _storage = storage;

    public IReadOnlyList<Order> GetAll() => _storage.Orders;

    public Order? GetById(int id) => _storage.Orders.FirstOrDefault(o => o.Id == id);

    public int GetNextId() => _storage.Orders.Count == 0 ? 1 : _storage.Orders.Max(o => o.Id) + 1;

    public void Add(Order order) => _storage.Orders.Add(order);
}
