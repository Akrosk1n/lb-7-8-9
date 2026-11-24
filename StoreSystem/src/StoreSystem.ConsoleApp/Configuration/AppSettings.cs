using System.Text.Json;

namespace StoreSystem.Configuration;

/// <summary>
/// Налаштування застосунку з файлу appsettings.json (артефакт конфігурації).
/// DataFilePath може бути відносним (відносно папки програми) або повним,
/// наприклад \\server\store\catalog.json для спільного файлового сервера.
/// </summary>
public class AppSettings
{
    public string StoreName { get; set; } = "Магазин";
    public string DataFilePath { get; set; } = "data/catalog.json";

    public static AppSettings Load(string path)
    {
        if (!File.Exists(path))
            return new AppSettings();

        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(path), options)
                   ?? new AppSettings();
        }
        catch (JsonException)
        {
            return new AppSettings();
        }
    }
}
