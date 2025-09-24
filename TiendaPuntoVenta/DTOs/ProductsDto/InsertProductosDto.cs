namespace TiendaPuntoVenta.DTOs.ProductsDto;

public class InsertProductosDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int UnitPrice { get; set; }
    public string? Image { get; set; }
    public int Stock { get; set; }
}