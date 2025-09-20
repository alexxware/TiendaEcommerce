using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;
using TiendaPuntoVenta.Repository;

namespace TiendaPuntoVenta.Service;

public class UserService:IUserService
{
    private readonly IUserRepository  _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddUser(InsertUserDto user)
    {
        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
            var userMapped = _mapper.Map<TblUsuario>(user);

            await _userRepository.Add(userMapped);
            await _userRepository.Save();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ValidateUser(LoginUserDto user)
    {
        var userDb = await _userRepository.SelectUserByEmail(user.Email!);
        if (userDb is null)
        {
            return false;
        }

        return VerifyPassword(user.Password!, userDb.Clave!);
    }

    private static bool VerifyPassword(string loginPassword, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(loginPassword, hashPassword);
    }
}