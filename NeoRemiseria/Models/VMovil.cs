using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class VMovil
{
    public uint Id { get; set; }

    public string Patente { get; set; } = null!;

    public ushort? Año { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public string? Titular { get; set; }

    public string? Dni { get; set; }
}
