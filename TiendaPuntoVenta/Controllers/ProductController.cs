using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.Service.ProductsService;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ProductosResponseDto> Get(int page = 1, int pageSize = 20)
    {
        var (list, total) = await _productService.GetAllProducts(page, pageSize);
        var response = new ProductosResponseDto
        {
            listProducts = list,
            totalProductos = total
        };
        return response;
    }
}