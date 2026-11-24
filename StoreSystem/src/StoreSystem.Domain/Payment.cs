namespace StoreSystem.Domain;

/// <summary>Абстрактний клас: оплата замовлення.</summary>
public abstract class Payment
{
    public decimal Amount { get; }

    protected Payment(decimal amount)
    {
        Amount = amount;
    }

    public abstract string MethodName { get; }

    /// <summary>Комісія за спосіб оплати. Поліморфна операція.</summary>
    public abstract decimal CalculateFee();

    public decimal TotalToPay => Amount + CalculateFee();
}
