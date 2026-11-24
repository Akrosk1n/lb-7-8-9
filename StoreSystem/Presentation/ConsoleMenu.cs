using StoreSystem.Business.Services;
using StoreSystem.Business.Validation;
using StoreSystem.Domain;

namespace StoreSystem.Presentation;

/// <summary>Рівень представлення: консольне меню. Працює лише з сервісами бізнес-логіки.</summary>
public class ConsoleMenu
{
    private readonly ProductService _productService;
    private readonly OrderService _orderService;
    private readonly StoreInfoService _infoService;

    public ConsoleMenu(ProductService productService, OrderService orderService, StoreInfoService infoService)
    {
        _productService = productService;
        _orderService = orderService;
        _infoService = infoService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            var choice = ConsoleInput.ReadString("Ваш вибір: ").Trim();
            Console.WriteLine();

            if (choice == "0")
            {
                Console.WriteLine("До побачення!");
                return;
            }

            try
            {
                switch (choice)
                {
                    case "1": ShowProducts(_productService.GetAllProducts()); break;
                    case "2": SearchByName(); break;
                    case "3": SearchByCategory(); break;
                    case "4": ShowOrders(); break;
                    case "5": AddProduct(); break;
                    case "6": CreateOrder(); break;
                    case "7": ChangeOrderStatus(); break;
                    case "8": ShowStoreInfo(); break;
                    default: Console.WriteLine("Невідомий пункт меню."); break;
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("========== Система автоматизації магазину ==========");
        Console.WriteLine("1. Переглянути всі товари");
        Console.WriteLine("2. Пошук товарів за назвою");
        Console.WriteLine("3. Пошук товарів за категорією");
        Console.WriteLine("4. Переглянути замовлення");
        Console.WriteLine("5. Додати товар (адміністратор)");
        Console.WriteLine("6. Оформити замовлення");
        Console.WriteLine("7. Змінити стан замовлення");
        Console.WriteLine("8. Загальна інформація про магазин");
        Console.WriteLine("0. Вихід");
        Console.WriteLine("====================================================");
    }

    private static void ShowProducts(IReadOnlyList<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товари не знайдено.");
            return;
        }

        foreach (var p in products)
        {
            Console.WriteLine(
                $"[{p.Id}] {p.Name} | {p.Category.Name} | {p.Price:0.00} грн | на складі: {p.Quantity}");
        }
    }

    private void SearchByName()
    {
        var text = ConsoleInput.ReadString("Введіть назву або її частину: ");
        ShowProducts(_productService.SearchByName(text));
    }

    private void SearchByCategory()
    {
        foreach (var c in _productService.GetAllCategories())
            Console.WriteLine($"{c.Id}. {c.Name}");

        var id = ConsoleInput.ReadInt("Введіть ID категорії: ");
        ShowProducts(_productService.SearchByCategory(id));
    }

    private void ShowOrders()
    {
        var orders = _orderService.GetAllOrders();
        if (orders.Count == 0)
        {
            Console.WriteLine("Замовлень ще немає.");
            return;
        }

        foreach (var o in orders)
        {
            Console.WriteLine(
                $"Замовлення №{o.Id} | {o.Customer.Name} | {o.CreatedAt:dd.MM.yyyy HH:mm} | " +
                $"стан: {OrderService.StatusName(o.Status)}");
            foreach (var item in o.Items)
                Console.WriteLine($"   - {item.Product.Name} x{item.Quantity} = {item.Total:0.00} грн");
            Console.WriteLine($"   Разом: {o.Total:0.00} грн");
        }
    }

    private void AddProduct()
    {
        var name = ConsoleInput.ReadString("Назва товару: ");
        var price = ConsoleInput.ReadDecimal("Ціна: ");
        var quantity = ConsoleInput.ReadInt("Кількість на складі: ");

        foreach (var c in _productService.GetAllCategories())
            Console.WriteLine($"{c.Id}. {c.Name}");
        var categoryId = ConsoleInput.ReadInt("ID категорії: ");

        var product = _productService.AddProduct(name, price, quantity, categoryId);
        Console.WriteLine($"Товар «{product.Name}» додано з ID {product.Id}.");
    }

    private void CreateOrder()
    {
        foreach (var c in _orderService.GetAllCustomers())
            Console.WriteLine($"Покупець {c.Id}: {c.Name}");
        var customerId = ConsoleInput.ReadInt("ID покупця: ");

        ShowProducts(_productService.GetAllProducts());
        var productId = ConsoleInput.ReadInt("ID товару: ");
        var quantity = ConsoleInput.ReadInt("Кількість: ");

        var order = _orderService.CreateOrder(customerId, productId, quantity);
        Console.WriteLine($"Замовлення №{order.Id} створено. Сума: {order.Total:0.00} грн.");
    }

    private void ChangeOrderStatus()
    {
        ShowOrders();
        var orderId = ConsoleInput.ReadInt("ID замовлення: ");

        Console.WriteLine("Нові стани:");
        foreach (OrderStatus s in Enum.GetValues(typeof(OrderStatus)))
            Console.WriteLine($"{(int)s}. {OrderService.StatusName(s)}");
        var statusNumber = ConsoleInput.ReadInt("Номер нового стану: ");

        if (!Enum.IsDefined(typeof(OrderStatus), statusNumber))
            throw new ValidationException("Такого стану не існує.");

        _orderService.ChangeStatus(orderId, (OrderStatus)statusNumber);
        Console.WriteLine("Стан замовлення змінено.");
    }

    private void ShowStoreInfo()
    {
        var s = _infoService.GetSummary();
        Console.WriteLine($"Товарів: {s.Products}");
        Console.WriteLine($"Категорій: {s.Categories}");
        Console.WriteLine($"Покупців: {s.Customers}");
        Console.WriteLine($"Замовлень: {s.Orders}");
        Console.WriteLine($"Вартість товарів на складі: {s.StockValue:0.00} грн");
    }
}
