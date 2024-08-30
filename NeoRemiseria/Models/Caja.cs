using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Caja
{
    public uint Turno { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal? Total { get; set; }
}
