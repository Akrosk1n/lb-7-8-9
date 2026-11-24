namespace StoreSystem.Domain;

/// <summary>Абстрактний клас: людина, що взаємодіє з магазином.</summary>
public abstract class Person
{
    public int Id { get; }
    public string Name { get; }
    public string Phone { get; }

    protected Person(int id, string name, string phone)
    {
        Id = id;
        Name = name;
        Phone = phone;
    }

    /// <summary>Роль людини в системі. Поліморфна операція.</summary>
    public abstract string Role { get; }

    public override string ToString() => $"{Role}: {Name} ({Phone})";
}
