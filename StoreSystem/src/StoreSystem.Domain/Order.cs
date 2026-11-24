namespace StoreSystem.Domain;

/// <summary>Замовлення покупця.</summary>
public class Order
{
    private readonly List<OrderItem> _items = new();

    public int Id { get; set; }
    public Customer Customer { get; }
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public DateTime CreatedAt { get; } = DateTime.Now;
    public Payment? Payment { get; set; }

    /// <summary>Позиції замовлення (композиція: життя позицій залежить від замовлення).</summary>
    public IReadOnlyList<OrderItem> Items => _items;

    /// <summary>Загальна вартість замовлення розраховується автоматично.</summary>
    public decimal Total => _items.Sum(i => i.Total);

    public Order(int id, Customer customer)
    {
        Id = id;
        Customer = customer;
    }

    public void AddItem(OrderItem item) => _items.Add(item);
}
