using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Service;

public interface IUserService
{
    Task<bool> AddUser(InsertUserDto user);
}