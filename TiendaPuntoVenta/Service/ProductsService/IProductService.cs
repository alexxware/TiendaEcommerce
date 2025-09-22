using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Service.ProductsService;

public interface IProductService
{
    Task<(List<ProductosDto>, int totalProductos)> GetAllProducts(int page,  int pageSize);
}