namespace TiendaPuntoVenta.DTOs
{
    public class ResponseLoginDto
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }
}
