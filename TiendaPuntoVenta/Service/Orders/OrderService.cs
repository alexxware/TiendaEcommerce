using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.Pedido;
using TiendaPuntoVenta.Models;
using TiendaPuntoVenta.Repository;

namespace TiendaPuntoVenta.Service.Orders;

public class OrderService: IOrderService
{
    private readonly ITiendaRepository _repositoryTienda;
    private readonly IMapper _mapper;

    public OrderService(ITiendaRepository repositoryTienda,  IMapper mapper)
    {
        _repositoryTienda = repositoryTienda;
        _mapper = mapper;
    }
    public async Task<ResponseResult> TakeOrder(List<PedidoDto> listOrder)
    {
        await _repositoryTienda.BeginTransactionAsync();
        try
        {
            foreach (var pedido in listOrder)
            {
                var product = await _repositoryTienda.SelectProductById(pedido.IdProducto);
                if (product == null) throw new Exception("Product not found");
                if (pedido.Cantidad > product.Stock) throw new Exception("Cantidad exceed");

                var order = _mapper.Map<TblPedido>(pedido);
                order.Total = pedido.Cantidad * product.PrecioUnitario;
            
                _repositoryTienda.TakeOrder(order);

                product.Stock -= pedido.Cantidad;
                _repositoryTienda.UpdateProduct(product);
                
            }
            
            await _repositoryTienda.Save();
            await _repositoryTienda.CommitAsync();
            return new ResponseResult { Result = true, Message = "Order added succesfully" };
        }
        catch (Exception e)
        {
            await _repositoryTienda.RollbackAsync();
            return new ResponseResult { Result = false, Message = e.Message };
        }
        
        
    }
}