using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Chofer
{
    public uint IdPersona { get; set; }

    public uint NumeroMovil { get; set; }

    public DateOnly FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public string? Observacion { get; set; }

    public string? Estado { get; set; }

    public DateOnly? Vencimiento_Carnet { get; set;}

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Movil NumeroMovilNavigation { get; set; } = null!;
}
