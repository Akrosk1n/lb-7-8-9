namespace StoreSystem.Domain;

/// <summary>Замовлення покупця.</summary>
public class Order
{
    public int Id { get; set; }
    public Customer Customer { get; }
    public List<OrderItem> Items { get; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Created;
    public DateTime CreatedAt { get; } = DateTime.Now;

    /// <summary>Загальна вартість замовлення розраховується автоматично.</summary>
    public decimal Total => Items.Sum(i => i.Total);

    public Order(int id, Customer customer)
    {
        Id = id;
        Customer = customer;
    }
}
