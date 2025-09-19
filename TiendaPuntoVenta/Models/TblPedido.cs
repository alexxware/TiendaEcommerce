using System;
using System.Collections.Generic;

namespace TiendaPuntoVenta.Models;

public partial class TblPedido
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public int? Total { get; set; }
}
