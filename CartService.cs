// Blazor Server 
// Data/CartService.cs

using System.Text;
using NewBlazorApp.Client.Pages;

namespace NewBlazorApp.Data;

public class CartService
{
    private readonly string _filePath;
    private readonly HttpClient _http;
    private List<Panel.Hosting> _selectedProducts = new();

    public IReadOnlyList<Panel.Hosting> SelectedProducts => _selectedProducts;

    public CartService(HttpClient http)
    {
        _http = http;
        
        var dataDir = Path.Combine(AppContext.BaseDirectory, "DataFiles");
        Directory.CreateDirectory(dataDir);

        _filePath = Path.Combine(dataDir, "cart.txt");

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "");
        }

        LoadFromFile();
    }

    public void AddProduct(Panel.Hosting hosting)
    {
        if (hosting != null)
        {
            _selectedProducts.Add(hosting);
            SaveToFile();
        }
    }

    public void Clear()
    {
        _selectedProducts.Clear();
        SaveToFile();
    }

    private void SaveToFile()
    {
        try
        {
            var lines = _selectedProducts
                .Select(p => $"{p.Name} | {p.Price} | {p.InetSpeed} | {p.L7} | {p.Api}");
            
            File.WriteAllLines(_filePath, lines, Encoding.UTF8);
            Console.WriteLine("Сохранили в: " + _filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении TXT: " + ex.Message);
        }
    }

    private void LoadFromFile()
    {
        try
        {
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath, Encoding.UTF8);
                _selectedProducts = lines
                    .Select(line =>
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 5)
                        {
                            return new Panel.Hosting
                            {
                                Name = parts[0].Trim(),
                                Price = parts[1].Trim(),
                                InetSpeed = parts[2].Trim(),
                                L7 = parts[3].Trim(),
                                Api = parts[4].Trim()
                            };
                        }
                        return null;
                    })
                    .Where(p => p != null)
                    .ToList()!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при загрузке TXT: " + ex.Message);
        }
    }


    public async Task RemovePost(Panel.Hosting hosting)
    {
        if (hosting == null) return;

        try
        {
            var res = await _http.DeleteAsync($"http://localhost:5013/api/cart/{hosting.Name}");

            if (res.IsSuccessStatusCode)
            {
                _selectedProducts.Remove(hosting);
                SaveToFile();
            }
            else
            {
                Console.WriteLine($"не удалось удалить с сервера " + res.StatusCode);
            }
        }
        catch (Exception err)
        {
            Console.WriteLine($"error while removing post : {err.Message}");
        }
    }    
}



