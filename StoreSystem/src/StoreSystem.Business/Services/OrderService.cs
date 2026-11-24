using StoreSystem.Business.Validation;
using StoreSystem.Data.Repositories;
using StoreSystem.Domain;

namespace StoreSystem.Business.Services;

/// <summary>
/// Логіка оформлення замовлень та керування їх життєвим циклом
/// (діаграма станів — лабораторна №5, діаграма послідовності — лабораторна №6).
/// </summary>
public class OrderService
{
    private readonly IOrderRepository _orders;
    private readonly IProductRepository _products;
    private readonly ICustomerRepository _customers;

    // Дозволені переходи між станами замовлення.
    private static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions = new()
    {
        [OrderStatus.Created]         = new[] { OrderStatus.AwaitingPayment, OrderStatus.Cancelled },
        [OrderStatus.AwaitingPayment] = new[] { OrderStatus.Paid, OrderStatus.Cancelled },
        [OrderStatus.Paid]            = new[] { OrderStatus.Preparing, OrderStatus.Cancelled },
        [OrderStatus.Preparing]       = new[] { OrderStatus.Ready },
        [OrderStatus.Ready]           = new[] { OrderStatus.Completed },
        [OrderStatus.Completed]       = Array.Empty<OrderStatus>(),
        [OrderStatus.Cancelled]       = Array.Empty<OrderStatus>()
    };

    public OrderService(IOrderRepository orders, IProductRepository products, ICustomerRepository customers)
    {
        _orders = orders;
        _products = products;
        _customers = customers;
    }

    public IReadOnlyList<Order> GetAllOrders() => _orders.GetAll();

    public IReadOnlyList<Customer> GetAllCustomers() => _customers.GetAll();

    public Order CreateOrder(int customerId, int productId, int quantity)
    {
        var customer = _customers.GetById(customerId)
            ?? throw new ValidationException("Покупця з таким ID не знайдено.");
        var product = _products.GetById(productId)
            ?? throw new ValidationException("Товар з таким ID не знайдено.");

        if (quantity <= 0)
            throw new ValidationException("Кількість повинна бути більшою за нуль.");
        if (product.Quantity < quantity)
            throw new ValidationException(
                $"Недостатньо товару на складі. Доступно: {product.Quantity} шт.");

        product.Reduce(quantity);

        var order = new Order(_orders.GetNextId(), customer);
        order.AddItem(new OrderItem(product, quantity));
        _orders.Add(order);
        return order;
    }

    public void ChangeStatus(int orderId, OrderStatus newStatus)
    {
        var order = _orders.GetById(orderId)
            ?? throw new ValidationException("Замовлення з таким ID не знайдено.");

        if (!AllowedTransitions[order.Status].Contains(newStatus))
            throw new ValidationException(
                $"Перехід зі стану «{StatusName(order.Status)}» у стан «{StatusName(newStatus)}» неможливий.");

        // Під час скасування товари повертаються на склад.
        if (newStatus == OrderStatus.Cancelled)
        {
            foreach (var item in order.Items)
                item.Product.Restore(item.Quantity);
        }

        order.Status = newStatus;
    }

    /// <summary>Оплата замовлення обраним способом. Спосіб оплати визначає комісію (поліморфізм).</summary>
    public Payment PayOrder(int orderId, bool byCard)
    {
        var order = _orders.GetById(orderId)
            ?? throw new ValidationException("Замовлення з таким ID не знайдено.");

        if (order.Status != OrderStatus.AwaitingPayment)
            throw new ValidationException("Оплатити можна лише замовлення у стані «Очікує оплати».");

        Payment payment = byCard ? new CardPayment(order.Total) : new CashPayment(order.Total);
        order.Payment = payment;
        ChangeStatus(orderId, OrderStatus.Paid);
        return payment;
    }

    public static string StatusName(OrderStatus status) => status switch
    {
        OrderStatus.Created => "Створене",
        OrderStatus.AwaitingPayment => "Очікує оплати",
        OrderStatus.Paid => "Оплачене",
        OrderStatus.Preparing => "Готується",
        OrderStatus.Ready => "Готове",
        OrderStatus.Completed => "Завершене",
        OrderStatus.Cancelled => "Скасоване",
        _ => status.ToString()
    };
}
