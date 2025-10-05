// Data/SiteService.cs


namespace NewBlazorApp.Data;

public class SiteService
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "urls.json");
    private List<SiteEntry> _sites = new();

    public SiteService() => Load();
    
    public void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _sites = System.Text.Json.JsonSerializer.Deserialize<List<SiteEntry>>(json) ??  new List<SiteEntry>();
        }
    }
    public void Save()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(_sites , new System.Text.Json.JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(_filePath, json);
    }

    public void AddSite(string url)
    {
        _sites.Add(new SiteEntry {Url = url , CreatedAt = DateTime.UtcNow});
        Save();
    }
    
    public List<SiteEntry> GetSites() => _sites;
    
    public class SiteEntry()
    {
        public string Url { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
