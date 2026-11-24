namespace StoreSystem.Domain;

/// <summary>Оплата карткою (листовий клас, комісія 1,5%).</summary>
public sealed class CardPayment : Payment
{
    private const decimal FeeRate = 0.015m;

    public CardPayment(decimal amount) : base(amount) { }

    public override string MethodName => "Картка";

    public override decimal CalculateFee() => Math.Round(Amount * FeeRate, 2);
}
