namespace StoreSystem.Domain;

/// <summary>Працівник магазину.</summary>
public class Employee : Person
{
    public string Position { get; }

    public Employee(int id, string name, string phone, string position) : base(id, name, phone)
    {
        Position = position;
    }

    public override string Role => $"Працівник ({Position})";
}
