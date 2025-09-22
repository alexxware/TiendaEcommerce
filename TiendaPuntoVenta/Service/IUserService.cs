using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Service;

public interface IUserService
{
    Task<bool> AddUser(InsertUserDto user);
    Task<ResponseLoginDto> ValidateUser(LoginUserDto user);
    Task<ResponseLoginDto> ValidateAdmin(LoginUserDto user);
}