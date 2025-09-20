using Microsoft.EntityFrameworkCore;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public class UserRepository: IUserRepository
{
    private readonly StoreDbContext _context;

    public UserRepository(StoreDbContext context)
    {
        _context = context;
    }
    
    public async Task Add(TblUsuario entity)
    {
        await _context.TblUsuarios.AddAsync(entity);
    }

    public async Task<TblUsuario?> SelectUserByEmail(string email)
    {
        return await _context.TblUsuarios.Where(e => e.Correo == email).FirstOrDefaultAsync();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}