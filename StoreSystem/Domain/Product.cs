namespace StoreSystem.Domain;

/// <summary>Товар магазину.</summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }

    public Product(int id, string name, decimal price, int quantity, Category category)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }
}
