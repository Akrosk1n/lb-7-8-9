using StoreSystem.Business.Validation;
using StoreSystem.Data.Repositories;
using StoreSystem.Domain;

namespace StoreSystem.Business.Services;

/// <summary>Логіка роботи з каталогом товарів.</summary>
public class ProductService
{
    private readonly IProductRepository _products;
    private readonly ICategoryRepository _categories;

    public ProductService(IProductRepository products, ICategoryRepository categories)
    {
        _products = products;
        _categories = categories;
    }

    public IReadOnlyList<Product> GetAllProducts() => _products.GetAll();

    public IReadOnlyList<Category> GetAllCategories() => _categories.GetAll();

    public IReadOnlyList<Product> SearchByName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ValidationException("Пошуковий запит не може бути порожнім.");
        return _products.FindByName(text.Trim());
    }

    public IReadOnlyList<Product> SearchByCategory(int categoryId)
    {
        if (_categories.GetById(categoryId) == null)
            throw new ValidationException("Категорію з таким ID не знайдено.");
        return _products.FindByCategory(categoryId);
    }

    public Product AddProduct(string name, decimal price, int quantity, int categoryId)
    {
        ProductValidator.Validate(name, price, quantity);

        var category = _categories.GetById(categoryId)
            ?? throw new ValidationException("Кожен товар повинен належати до існуючої категорії.");

        var product = new Product(_products.GetNextId(), name.Trim(), price, quantity, category);
        _products.Add(product);
        return product;
    }
}
