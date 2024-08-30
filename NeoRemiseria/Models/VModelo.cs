using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class VModelo
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Marca { get; set; }
}
