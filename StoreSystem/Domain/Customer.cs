namespace StoreSystem.Domain;

/// <summary>Покупець.</summary>
public class Customer : Person
{
    public Customer(int id, string name, string phone) : base(id, name, phone) { }

    public override string Role => "Покупець";
}
