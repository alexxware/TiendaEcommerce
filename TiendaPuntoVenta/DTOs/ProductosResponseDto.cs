namespace TiendaPuntoVenta.DTOs;

public class ProductosResponseDto
{
    public List<ProductosDto> listProducts { get; set; }
    public int totalProductos { get; set; }
}