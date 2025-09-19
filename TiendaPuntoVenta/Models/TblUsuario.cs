using System;
using System.Collections.Generic;

namespace TiendaPuntoVenta.Models;

public partial class TblUsuario
{
    public int Id { get; set; }

    public string? Usuario { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public string? Administrador { get; set; }

    public string? Estatus { get; set; }

    public DateTime? FechaCaptura { get; set; }
}
