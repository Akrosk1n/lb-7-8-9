using System.Globalization;

namespace StoreSystem.Presentation;

/// <summary>Допоміжні методи для безпечного зчитування даних із консолі.</summary>
public static class ConsoleInput
{
    public static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var value))
                return value;
            Console.WriteLine("Помилка: введіть ціле число.");
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var text = (Console.ReadLine() ?? string.Empty).Replace(',', '.');
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out var value))
                return value;
            Console.WriteLine("Помилка: введіть число.");
        }
    }
}
