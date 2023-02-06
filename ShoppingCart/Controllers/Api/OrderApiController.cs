using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers.Api;

[ApiController]
[Route("api/order")]
public class OrderApiController : ControllerBase
{
    private readonly ShoppingCartContext _context;

    public OrderApiController(ShoppingCartContext context)
    {
        _context = context;
    }

    // GET: api/order
    [HttpGet]
    public IEnumerable<Order> Get()
    {
        return _context.Orders.ToList();
    }
}