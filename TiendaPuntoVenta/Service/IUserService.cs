using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Service;

public interface IUserService
{
    Task<bool> AddUser(InsertUserDto user);
    Task<bool> ValidateUser(LoginUserDto user);
}