namespace StoreSystem.Domain;

/// <summary>Позиція замовлення.</summary>
public class OrderItem
{
    public Product Product { get; }
    public int Quantity { get; }

    /// <summary>Вартість позиції розраховується автоматично.</summary>
    public decimal Total => Product.Price * Quantity;

    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}
