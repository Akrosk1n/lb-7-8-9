using StoreSystem.Data.Repositories;
using StoreSystem.Domain;

namespace StoreSystem.Business.Services;

/// <summary>Формує загальну інформацію про магазин.</summary>
public class StoreInfoService
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly ICustomerRepository _customers;
    private readonly IEmployeeRepository _employees;
    private readonly IOrderRepository _orders;

    public StoreInfoService(
        IProductRepository products,
        ICategoryRepository categories,
        ICustomerRepository customers,
        IEmployeeRepository employees,
        IOrderRepository orders)
    {
        _products = products;
        _categories = categories;
        _customers = customers;
        _employees = employees;
        _orders = orders;
    }

    public (int Products, int Categories, int Customers, int Orders, decimal StockValue) GetSummary()
    {
        var stockValue = _products.GetAll().Sum(p => p.Price * p.Quantity);
        return (_products.GetAll().Count, _categories.GetAll().Count,
                _customers.GetAll().Count, _orders.GetAll().Count, stockValue);
    }

    /// <summary>Усі люди системи: покупці та працівники (поліморфна колекція Person).</summary>
    public IReadOnlyList<Person> GetPeople() =>
        _customers.GetAll().Cast<Person>().Concat(_employees.GetAll()).ToList();
}
