namespace StoreSystem.Domain;

/// <summary>Стани замовлення (див. діаграму станів, лабораторна робота №5).</summary>
public enum OrderStatus
{
    Created,          // Створене
    AwaitingPayment,  // Очікує оплати
    Paid,             // Оплачене
    Preparing,        // Готується
    Ready,            // Готове
    Completed,        // Завершене
    Cancelled         // Скасоване
}
