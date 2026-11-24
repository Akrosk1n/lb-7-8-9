namespace StoreSystem.Business.Validation;

/// <summary>Перевірка бізнес-правил для товару (документ «Бачення», лабораторна №4).</summary>
public static class ProductValidator
{
    public static void Validate(string name, decimal price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Назва товару не може бути порожньою.");
        if (price < 0)
            throw new ValidationException("Ціна товару не може бути від'ємною.");
        if (quantity < 0)
            throw new ValidationException("Кількість товару не може бути від'ємною.");
    }
}
