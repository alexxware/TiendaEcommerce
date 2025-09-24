using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.ProductsDto;

namespace TiendaPuntoVenta.Service.ProductsService;

public interface IProductService
{
    Task<(List<ProductosDto>, int totalProductos)> GetAllProducts(int page,  int pageSize);
    Task<bool> AddProduct(InsertProductosDto producto, IFormFile? file);
    Task<ResponseResult> UpdateProduct(ProductosDto producto, IFormFile? file);
    Task<ResponseResult> DeleteProduct(int id);
}