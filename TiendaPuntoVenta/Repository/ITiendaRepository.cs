using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.ProductsDto;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public interface ITiendaRepository
{
    Task Add(TblUsuario entity);
    Task<TblUsuario?> SelectUserByEmail(string email);
    Task<(List<TblProducto>, int totalProductos)> GetAllProductos(int page,  int pageSize);
    Task<bool> AddProduct(TblProducto producto);
    void UpdateProduct(TblProducto producto);
    Task<TblProducto?> SelectProductById(int id);
    void DeleteProduct(TblProducto producto);
    Task Save();
}