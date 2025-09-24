using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.Pedido;

namespace TiendaPuntoVenta.Service.Orders;

public interface IOrderService
{
    Task<ResponseResult> TakeOrder(List<PedidoDto> pedidos);
}