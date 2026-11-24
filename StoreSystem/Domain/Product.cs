namespace StoreSystem.Domain;

/// <summary>Товар магазину.</summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; private set; }
    public Category Category { get; set; }

    public Product(int id, string name, decimal price, int quantity, Category category)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    /// <summary>Списує товар зі складу.</summary>
    public void Reduce(int amount)
    {
        if (amount <= 0 || amount > Quantity)
            throw new ArgumentOutOfRangeException(nameof(amount));
        Quantity -= amount;
    }

    /// <summary>Повертає товар на склад.</summary>
    public void Restore(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        Quantity += amount;
    }
}
