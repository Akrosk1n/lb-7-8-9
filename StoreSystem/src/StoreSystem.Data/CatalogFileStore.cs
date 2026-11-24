using System.Text.Encodings.Web;
using System.Text.Json;
using StoreSystem.Domain;

namespace StoreSystem.Data;

/// <summary>
/// Зберігає та завантажує каталог (категорії й товари) у файлі JSON.
/// Файл catalog.json є артефактом даних, який розгортається на вузлі зберігання.
/// </summary>
public class CatalogFileStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly string _path;

    public CatalogFileStore(string path) => _path = path;

    public string FilePath => _path;

    /// <summary>Завантажує каталог. Повертає false, якщо файлу немає або він пошкоджений.</summary>
    public bool Load(DataStorage storage)
    {
        if (!File.Exists(_path))
            return false;

        CatalogDto? dto;
        try
        {
            dto = JsonSerializer.Deserialize<CatalogDto>(File.ReadAllText(_path), Options);
        }
        catch (JsonException)
        {
            return false;
        }

        if (dto == null || dto.Categories.Count == 0)
            return false;

        storage.Categories.Clear();
        storage.Products.Clear();

        foreach (var c in dto.Categories)
            storage.Categories.Add(new Category(c.Id, c.Name));

        foreach (var p in dto.Products)
        {
            var category = storage.Categories.FirstOrDefault(c => c.Id == p.CategoryId);
            if (category == null)
                continue;
            storage.Products.Add(new Product(p.Id, p.Name, p.Price, p.Quantity, category));
        }

        return true;
    }

    public void Save(DataStorage storage)
    {
        var dto = new CatalogDto
        {
            Categories = storage.Categories
                .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
                .ToList(),
            Products = storage.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    CategoryId = p.Category.Id
                })
                .ToList()
        };

        var directory = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(_path, JsonSerializer.Serialize(dto, Options));
    }

    private class CatalogDto
    {
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ProductDto> Products { get; set; } = new();
    }

    private class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    private class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
