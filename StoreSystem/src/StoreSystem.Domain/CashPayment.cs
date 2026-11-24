namespace StoreSystem.Domain;

/// <summary>Оплата готівкою (листовий клас, без комісії).</summary>
public sealed class CashPayment : Payment
{
    public CashPayment(decimal amount) : base(amount) { }

    public override string MethodName => "Готівка";

    public override decimal CalculateFee() => 0m;
}
