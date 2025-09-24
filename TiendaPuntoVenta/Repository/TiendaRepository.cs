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
        var query = _context.TblProductos.Where(p => p.Estatus == null);
        var listaProductos = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await query.CountAsync();

        return (listaProductos, total);
    }

    public async Task<bool> AddProduct(TblProducto producto)
    {
        try
        {
            await _context.TblProductos.AddAsync(producto);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void UpdateProduct(TblProducto producto)
    {
        _context.TblProductos.Attach(producto);
        _context.Entry(producto).State = EntityState.Modified;
    }

    public async Task<TblProducto?> SelectProductById(int id)
    {
        return await _context.TblProductos.FindAsync(id);
    }

    public void DeleteProduct(TblProducto producto)
    {
        //_context.TblProductos.Remove(producto);
        _context.TblProductos.Attach(producto);
        _context.Entry(producto).State = EntityState.Modified;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}