using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.ProductsDto;
using TiendaPuntoVenta.Service.ProductsService;

namespace TiendaPuntoVenta.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<InsertProductosDto> _validatorInsertProduct;
    private readonly IValidator<ProductosDto> _validatorProduct;
    public ProductController(IProductService productService,  
        IValidator<InsertProductosDto> validatorInsertProduct,
        IValidator<ProductosDto> validatorProduct)
    {
        _productService = productService;
        _validatorInsertProduct = validatorInsertProduct;
        _validatorProduct = validatorProduct;
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromForm] InsertProductosDto producto, IFormFile? imageFile)
    {
        var validationResult = await _validatorInsertProduct.ValidateAsync(producto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var res = await _productService.AddProduct(producto, imageFile);
        if (!res) return BadRequest();
        return Ok(new { Message = "Product Added Successfully" });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductosDto producto,  IFormFile? imageFile)
    {
        var validationResult = await _validatorProduct.ValidateAsync(producto);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
        
        var res = await _productService.UpdateProduct(producto, imageFile);
        if (!res.Result) return BadRequest(res.Message);
        
        return Ok(new { res.Message });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id <= 0) return BadRequest("Id must be greater than zero");
        
        var  res = await _productService.DeleteProduct(id);
        if(!res.Result) return BadRequest(res.Message);
        
        return Ok(new { res.Message });
    }
}