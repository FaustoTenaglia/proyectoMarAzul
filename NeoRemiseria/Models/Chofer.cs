using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Chofer
{
    public uint Id {get; set;}
    
    public uint IdPersona { get; set; }

    public uint? NumeroMovil { get; set; }

    public DateOnly FechaDesde { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public DateOnly? FechaHasta { get; set; }

    public string? Observacion { get; set; }

    public char? Estado { get; set; } = 'A';

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Movil NumeroMovilNavigation { get; set; } = null!;
}
