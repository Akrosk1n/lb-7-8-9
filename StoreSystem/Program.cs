using System.Text;
using StoreSystem.Business.Services;
using StoreSystem.Data;
using StoreSystem.Data.Repositories;
using StoreSystem.Presentation;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Рівень даних
var storage = new DataStorage();
ICategoryRepository categoryRepository = new CategoryRepository(storage);
IProductRepository productRepository = new ProductRepository(storage);
ICustomerRepository customerRepository = new CustomerRepository(storage);
IOrderRepository orderRepository = new OrderRepository(storage);

// Рівень бізнес-логіки
var productService = new ProductService(productRepository, categoryRepository);
var orderService = new OrderService(orderRepository, productRepository, customerRepository);
var infoService = new StoreInfoService(productRepository, categoryRepository, customerRepository, orderRepository);

// Рівень представлення
var menu = new ConsoleMenu(productService, orderService, infoService);
menu.Run();
