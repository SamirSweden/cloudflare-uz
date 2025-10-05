// Controller/CartController.cs


using Microsoft.AspNetCore.Mvc;
using NewBlazorApp.Client.Pages;
using NewBlazorApp.Data;

namespace NewBlazorApp.Controller;


[ApiController]
[Route("api/[controller]")]
public class CartControllers : ControllerBase
{
    private readonly CartService _cartService;

    public CartControllers(CartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public IActionResult GetCart() => Ok(_cartService.SelectedProducts);

    [HttpPost]
    public IActionResult Add([FromBody] Panel.Hosting hosting)
    {
        _cartService.AddProduct(hosting);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Clear()
    {
        _cartService.Clear();
        return Ok();
    }
}
