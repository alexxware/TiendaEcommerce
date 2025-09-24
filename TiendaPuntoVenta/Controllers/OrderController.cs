using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs.Pedido;
using TiendaPuntoVenta.Service.Orders;
using TiendaPuntoVenta.Service.ProductsService;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> TakeOrder([FromBody] List<PedidoDto>  pedidos)
    {
        var response = await _orderService.TakeOrder(pedidos);
        if (!response.Result) return BadRequest(response.Message);
        
        return Ok(new { Message = response.Message });
    }
}