using AutoMapper;
using TiendaPuntoVenta.DTOs;
using TiendaPuntoVenta.DTOs.ProductsDto;
using TiendaPuntoVenta.Models;
using TiendaPuntoVenta.Repository;

namespace TiendaPuntoVenta.Service.ProductsService;

public class ProductService: IProductService
{
    private readonly ITiendaRepository _tiendaRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(ITiendaRepository tienda,  IMapper mapper,  IHttpContextAccessor httpContextAccessor)
    {
        _tiendaRepository = tienda;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<(List<ProductosDto>, int totalProductos)> GetAllProducts(int  page, int pageSize)
    {
        var (response, total) = await _tiendaRepository.GetAllProductos(page, pageSize);
        
        List<ProductosDto> listaProductos = new List<ProductosDto>();

        var request = _httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
        
        foreach (var producto in response)
        {
            var productoDto = _mapper.Map<ProductosDto>(producto);
            if (!string.IsNullOrEmpty(productoDto.Image)) productoDto.Image = $"{baseUrl}{productoDto.Image}";
            listaProductos.Add(productoDto);
        }
        
        return (listaProductos, total);
    }

    public async Task<bool> AddProduct(InsertProductosDto producto,  IFormFile? file)
    {
        var product = _mapper.Map<TblProducto>(producto);
        if (file != null)
        {
            var uniqueFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("images", uniqueFile);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            product.Imagen = $"/{filePath.Replace("\\", "/")}";
        }
        var res =  await _tiendaRepository.AddProduct(product);
        await _tiendaRepository.Save();
        return res;
    }

    public async Task<ResponseResult> UpdateProduct(ProductosDto producto, IFormFile? file)
    {
        var productoBd = await _tiendaRepository.SelectProductById(producto.Id);
        if (productoBd == null) return new ResponseResult { Result = false, Message = "Product not found" };
        
        if (file != null)
        {
            var uniqueFile = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("images", uniqueFile);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            productoBd.Imagen = $"/{filePath.Replace("\\", "/")}";
        }
        else
        {
            productoBd.Imagen =  producto.Image;
        }

        productoBd.Nombre = producto.Name;
        productoBd.Stock = producto.Stock;
        productoBd.PrecioUnitario = producto.UnitPrice;
        productoBd.Descripcion = producto.Description;
        
        try
        {
            _tiendaRepository.UpdateProduct(productoBd);
            await _tiendaRepository.Save();
            return new ResponseResult { Result = true, Message = "Product Updated Successfully" };
        }
        catch (Exception e)
        {
            return new ResponseResult { Result = false, Message = e.Message };
        }
        
    }

    public async Task<ResponseResult> DeleteProduct(int id)
    {
        var productoBd = await _tiendaRepository.SelectProductById(id);
        if (productoBd == null) return new ResponseResult { Result = false, Message = "Product not found" };
        productoBd.Estatus = "b";
        _tiendaRepository.DeleteProduct(productoBd);
        await _tiendaRepository.Save();
        return  new ResponseResult { Result = true, Message = "Product Deleted Successfully" };
    }
}