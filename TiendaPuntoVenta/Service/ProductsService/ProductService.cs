using AutoMapper;
using TiendaPuntoVenta.DTOs;
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
}