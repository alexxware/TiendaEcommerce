namespace TiendaPuntoVenta.DTOs;

public class ProductosDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public string Image { get; set; }
    public int Stock { get; set; }
}