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

        CreateMap<TblProducto, ProductosDto>()
            .ForMember(from => from.Id, to => to.MapFrom(t => t.Id))
            .ForMember(from => from.Name, to => to.MapFrom(t => t.Nombre))
            .ForMember(from => from.Description, to => to.MapFrom(t => t.Descripcion))
            .ForMember(from => from.UnitPrice, to => to.MapFrom(t => t.PrecioUnitario))
            .ForMember(from => from.Image, to => to.MapFrom(t => t.Imagen))
            .ForMember(from => from.Stock, to => to.MapFrom(t => t.Stock));
    }
}