using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;
using TiendaPuntoVenta.Repository;
using TiendaPuntoVenta.Service.Auth;

namespace TiendaPuntoVenta.Service;

public class UserService:IUserService
{
    private readonly ITiendaRepository  _tiendaRepository;
    private readonly IMapper _mapper;
    private readonly IJwtHelper _jwtHelper;

    public UserService(ITiendaRepository tiendaRepository, IMapper mapper, IJwtHelper jwt)
    {
        _tiendaRepository = tiendaRepository;
        _mapper = mapper;
        _jwtHelper = jwt;
    }
    
    public async Task<bool> AddUser(InsertUserDto user)
    {
        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            var userMapped = _mapper.Map<TblUsuario>(user);

            await _tiendaRepository.Add(userMapped);
            await _tiendaRepository.Save();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<ResponseLoginDto> ValidateUser(LoginUserDto user)
    {
        var userDb = await _tiendaRepository.SelectUserByEmail(user.Email!);
        if (userDb is null || !VerifyPassword(user.Password!, userDb.Clave!))
        {
            return new ResponseLoginDto();
        }

        var token = _jwtHelper.GenerateToken(userId: userDb.Id.ToString(), email: userDb.Correo!, role: "User");


        return new ResponseLoginDto {
            Token = token,
            User = new UserDto
            {
                Id = userDb.Id,
                Usuario = userDb.Usuario,
                Correo = userDb.Correo
            }
        };
    }

    public async Task<ResponseLoginDto> ValidateAdmin(LoginUserDto user)
    {
        var userDb = await _tiendaRepository.SelectUserByEmail(user.Email!);
        if (userDb is not null && VerifyPassword(user.Password!, userDb.Clave!))
        {
            if (userDb.Administrador != "admin") return new ResponseLoginDto();
            var token = _jwtHelper.GenerateToken(userDb.Id.ToString(), userDb.Correo!, role: "Admin");
            return new ResponseLoginDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = userDb.Id,
                    Usuario = userDb.Usuario,
                    Correo = userDb.Correo
                }
            };
        }

        return new ResponseLoginDto();
    }

    private static bool VerifyPassword(string loginPassword, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(loginPassword, hashPassword);
    }
}