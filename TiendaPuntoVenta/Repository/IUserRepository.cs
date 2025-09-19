using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public interface IUserRepository
{
    Task Add(TblUsuario entity);
    Task Save();
}