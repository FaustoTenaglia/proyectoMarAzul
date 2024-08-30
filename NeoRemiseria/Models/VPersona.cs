using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class VPersona
{
    public string Dni { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Nombre { get; set; }

    public int? Edad { get; set; }

    public string? Calle { get; set; }

    public uint? Numero { get; set; }

    public string? Localidad { get; set; }
}
