// Controller/SitesController.cs


using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewBlazorApp.Controller
{
    [ApiController] 
    [Route("api/[controller]")]
    public class SitesController : ControllerBase
    {
        private readonly string _filePath = "sites.txt";

        public SitesController()
        {
            // Выводим полный путь к файлу при создании контроллера
            var fullPath = System.IO.Path.GetFullPath(_filePath);
            System.Console.WriteLine($"Файл будет сохранен по пути: {fullPath}");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSite([FromBody] string url)
        {
            try
            {
                // Выводим путь перед сохранением
                var fullPath = System.IO.Path.GetFullPath(_filePath);
                System.Console.WriteLine($"Сохранение в файл: {fullPath}");
                System.Console.WriteLine($"Добавляемый URL: {url}");

                // Валидация URL
                if (!System.Uri.TryCreate(url, System.UriKind.Absolute, out _))
                {
                    System.Console.WriteLine("Некорректный URL");
                    return BadRequest("Некорректный URL");
                }

                // Сохранение в файл
                await System.IO.File.AppendAllTextAsync(_filePath, $"{url}{System.Environment.NewLine}");
                
                System.Console.WriteLine("URL успешно сохранен");
                return Ok("Сайт сохранен");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSites()
        {
            try
            {
                var fullPath = System.IO.Path.GetFullPath(_filePath);
                System.Console.WriteLine($"Проверка файла: {fullPath}");

                if (!System.IO.File.Exists(_filePath))
                {
                    System.Console.WriteLine("Файл не существует");
                    return Ok(System.Array.Empty<string>());
                }

                var sites = await System.IO.File.ReadAllLinesAsync(_filePath);
                System.Console.WriteLine($"Найдено {sites.Length} сайтов");
                
                return Ok(System.Linq.Enumerable.Where(sites, s => !string.IsNullOrWhiteSpace(s)));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("path")]
        public IActionResult GetFilePath()
        {
            var fullPath = System.IO.Path.GetFullPath(_filePath);
            System.Console.WriteLine($"Запрос пути файла: {fullPath}");
            return Ok(new { FilePath = fullPath });
        }
    }
}
