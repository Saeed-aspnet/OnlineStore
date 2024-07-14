using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Services;

namespace OnlineStore.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> BuyProduct(int productId, int userId)
    {
        var result = await _orderService.BuyProduct(productId, userId);

        return Ok(result);
    }
}