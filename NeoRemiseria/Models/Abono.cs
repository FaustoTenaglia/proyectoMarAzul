using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Abono
{
    public string Tipo { get; set; } = null!;

    public byte Cuotas { get; set; }

    public decimal Importe { get; set; }
}
