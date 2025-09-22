using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public interface ITiendaRepository
{
    Task Add(TblUsuario entity);
    Task<TblUsuario?> SelectUserByEmail(string email);
    Task<(List<TblProducto>, int totalProductos)> GetAllProductos(int page,  int pageSize);
    Task Save();
}