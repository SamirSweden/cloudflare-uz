// Controller/CartController.cs



using Microsoft.AspNetCore.Mvc;
using NewBlazorApp.Data;


namespace NewBlazorApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly  SiteService _siteService;

        public SiteController(SiteService siteService)
        {
            _siteService = siteService;
        }


        [HttpPost("add")]
        public ActionResult Add([FromBody] string url)
        {
            _siteService.AddSite(url);
            return Ok(new {message = "✅ Site successfully proxied"});
        }


        [HttpGet]
        public ActionResult GetAll() => Ok(_siteService.GetSites());
    }
}

