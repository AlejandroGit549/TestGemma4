using System;
using System.Collections.Generic;

namespace WeeTestIA.Domain;

public partial class CatPerfil
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualización { get; set; }
}
