using AutoMapper;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Automappers;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<InsertUserDto, TblUsuario>()
            .ForMember(from => from.Usuario, to => to.MapFrom(t => t.Username))
            .ForMember(from => from.Correo, to => to.MapFrom(t => t.Email))
            .ForMember(from => from.Clave, to => to.MapFrom(t => t.Password));
    }
}