using StoreSystem.Domain;

namespace StoreSystem.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataStorage _storage;

    public EmployeeRepository(DataStorage storage) => _storage = storage;

    public IReadOnlyList<Employee> GetAll() => _storage.Employees;

    public Employee? GetById(int id) => _storage.Employees.FirstOrDefault(e => e.Id == id);

    public void Add(Employee item) => _storage.Employees.Add(item);
}
