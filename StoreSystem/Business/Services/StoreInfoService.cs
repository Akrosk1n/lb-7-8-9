using StoreSystem.Data.Repositories;

namespace StoreSystem.Business.Services;

/// <summary>Формує загальну інформацію про магазин.</summary>
public class StoreInfoService
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;
    private readonly ICustomerRepository _customers;
    private readonly IOrderRepository _orders;

    public StoreInfoService(
        IProductRepository products,
        ICategoryRepository categories,
        ICustomerRepository customers,
        IOrderRepository orders)
    {
        _products = products;
        _categories = categories;
        _customers = customers;
        _orders = orders;
    }

    public (int Products, int Categories, int Customers, int Orders, decimal StockValue) GetSummary()
    {
        var stockValue = _products.GetAll().Sum(p => p.Price * p.Quantity);
        return (_products.GetAll().Count, _categories.GetAll().Count,
                _customers.GetAll().Count, _orders.GetAll().Count, stockValue);
    }
}
