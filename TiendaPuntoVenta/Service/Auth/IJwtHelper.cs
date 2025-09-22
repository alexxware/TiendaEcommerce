namespace TiendaPuntoVenta.Service.Auth
{
    public interface IJwtHelper
    {
        string GenerateToken(string userId, string email, string role);
    }
}
