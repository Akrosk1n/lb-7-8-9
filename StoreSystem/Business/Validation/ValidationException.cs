namespace StoreSystem.Business.Validation;

/// <summary>Помилка порушення бізнес-правил.</summary>
public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
