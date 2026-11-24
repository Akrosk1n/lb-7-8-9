namespace StoreSystem.Domain;

/// <summary>Покупець.</summary>
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public Customer(int id, string name, string phone)
    {
        Id = id;
        Name = name;
        Phone = phone;
    }
}
