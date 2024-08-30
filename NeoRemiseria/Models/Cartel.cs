using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Cartel
{
    public uint NumeroMovil { get; set; }

    public DateOnly FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public string? Observacion { get; set; }

    public string? Estado { get; set; }

    public virtual Movil NumeroMovilNavigation { get; set; } = null!;
}
