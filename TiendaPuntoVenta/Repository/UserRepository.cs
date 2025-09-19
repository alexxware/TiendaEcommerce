using TiendaPuntoVenta.Models;

namespace TiendaPuntoVenta.Repository;

public class UserRepository: IUserRepository
{
    private StoreDbContext _context;

    public UserRepository(StoreDbContext context)
    {
        _context = context;
    }
    
    public async Task Add(TblUsuario entity)
    {
        await _context.TblUsuarios.AddAsync(entity);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}