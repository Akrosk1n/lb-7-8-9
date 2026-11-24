using System.Text;
using StoreSystem.Business.Services;
using StoreSystem.Configuration;
using StoreSystem.Data;
using StoreSystem.Data.Repositories;
using StoreSystem.Presentation;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Артефакт конфігурації: appsettings.json
var settings = AppSettings.Load(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

// Артефакт даних: catalog.json (шлях може вказувати на спільний файловий сервер)
var dataPath = Path.Combine(AppContext.BaseDirectory, settings.DataFilePath);
var storage = new DataStorage();
var catalogStore = new CatalogFileStore(dataPath);

Console.WriteLine(catalogStore.Load(storage)
    ? $"Каталог завантажено з файлу: {dataPath}"
    : "Файл каталогу не знайдено або пошкоджено, використано тестові дані.");

// Рівень даних
ICategoryRepository categoryRepository = new CategoryRepository(storage);
IProductRepository productRepository = new ProductRepository(storage);
ICustomerRepository customerRepository = new CustomerRepository(storage);
IEmployeeRepository employeeRepository = new EmployeeRepository(storage);
IOrderRepository orderRepository = new OrderRepository(storage);

// Рівень бізнес-логіки
var productService = new ProductService(productRepository, categoryRepository);
var orderService = new OrderService(orderRepository, productRepository, customerRepository);
var infoService = new StoreInfoService(productRepository, categoryRepository, customerRepository, employeeRepository, orderRepository);

// Рівень представлення
Console.WriteLine();
Console.WriteLine($"*** {settings.StoreName} ***");
var menu = new ConsoleMenu(productService, orderService, infoService);
menu.Run();

// Збереження каталогу під час завершення роботи
try
{
    catalogStore.Save(storage);
    Console.WriteLine($"Каталог збережено: {dataPath}");
}
catch (IOException ex)
{
    Console.WriteLine($"Не вдалося зберегти каталог: {ex.Message}");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"Немає доступу до файлу каталогу: {ex.Message}");
}
