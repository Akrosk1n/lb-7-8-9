using StoreSystem.Domain;

namespace StoreSystem.Data;

/// <summary>
/// Сховище даних у пам'яті з тестовими даними.
/// Єдине місце, яке безпосередньо тримає колекції сутностей.
/// </summary>
public class DataStorage
{
    public List<Category> Categories { get; } = new();
    public List<Product> Products { get; } = new();
    public List<Customer> Customers { get; } = new();
    public List<Order> Orders { get; } = new();

    public DataStorage()
    {
        var phones = new Category(1, "Смартфони");
        var laptops = new Category(2, "Ноутбуки");
        var accessories = new Category(3, "Аксесуари");
        Categories.AddRange(new[] { phones, laptops, accessories });

        Products.Add(new Product(1, "Смартфон Galaxy A54", 15999m, 10, phones));
        Products.Add(new Product(2, "Смартфон iPhone 13", 27999m, 5, phones));
        Products.Add(new Product(3, "Ноутбук Lenovo IdeaPad", 24500m, 4, laptops));
        Products.Add(new Product(4, "Ноутбук ASUS VivoBook", 21900m, 6, laptops));
        Products.Add(new Product(5, "Бездротові навушники", 1850m, 25, accessories));
        Products.Add(new Product(6, "Чохол для смартфона", 350m, 40, accessories));

        Customers.Add(new Customer(1, "Іваненко Петро", "+380501112233"));
        Customers.Add(new Customer(2, "Коваленко Марія", "+380671234567"));
    }
}
