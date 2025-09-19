namespace TiendaPuntoVenta.DTOs;

public class PedidosDto
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdProduct { get; set; }
    public int Quantity { get; set; }
    public int Total { get; set; }
    public DateTime Captured { get; set; }
}