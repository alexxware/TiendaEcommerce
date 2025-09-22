using Microsoft.EntityFrameworkCore;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public class TiendaRepository: ITiendaRepository
{
    private readonly StoreDbContext _context;

    public TiendaRepository(StoreDbContext context)
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

    public async Task<(List<TblProducto>, int totalProductos)> GetAllProductos(int page, int pageSize)
    {
        var listaProductos = await _context.TblProductos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await _context.TblProductos.CountAsync();

        return (listaProductos, total);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}