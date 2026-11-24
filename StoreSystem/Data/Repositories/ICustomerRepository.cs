using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(int id);
}
