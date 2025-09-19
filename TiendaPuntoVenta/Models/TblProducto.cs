using System;
using System.Collections.Generic;

namespace TiendaPuntoVenta.Models;

public partial class TblProducto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? PrecioUnitario { get; set; }

    public string? Imagen { get; set; }

    public int? Stock { get; set; }

    public string? Estatus { get; set; }

    public DateTime? FechaCaptura { get; set; }
}
