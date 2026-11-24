using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    int GetNextId();
}
