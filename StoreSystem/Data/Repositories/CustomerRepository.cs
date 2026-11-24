using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataStorage _storage;

    public CustomerRepository(DataStorage storage) => _storage = storage;

    public IReadOnlyList<Customer> GetAll() => _storage.Customers;

    public Customer? GetById(int id) => _storage.Customers.FirstOrDefault(c => c.Id == id);
}
