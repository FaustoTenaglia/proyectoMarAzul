using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Movimiento
{
    public uint Id { get; set; }

    public decimal? Importe { get; set; }

    public DateOnly? Fecha { get; set; }

    public uint? Turno { get; set; }

    public uint IdServicio { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
