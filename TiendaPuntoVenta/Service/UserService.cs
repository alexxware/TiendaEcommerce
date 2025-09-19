using AutoMapper;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;
using TiendaPuntoVenta.Repository;

namespace TiendaPuntoVenta.Service;

public class UserService:IUserService
{
    private IUserRepository  _userRepository;
    private IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddUser(InsertUserDto userInsert)
    {
        try
        {
            var user = _mapper.Map<TblUsuario>(userInsert);

            await _userRepository.Add(user);
            await _userRepository.Save();
            return true;
        }
        catch
        {
            return false;
        }
    }
}