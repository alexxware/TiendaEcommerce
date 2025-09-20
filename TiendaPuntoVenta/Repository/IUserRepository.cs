using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public interface IUserRepository
{
    Task Add(TblUsuario entity);
    Task<TblUsuario?> SelectUserByEmail(string email);
    Task Save();
}