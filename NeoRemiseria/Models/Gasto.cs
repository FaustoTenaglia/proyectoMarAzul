using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Gasto
{
    public DateOnly? Fecha { get; set; }

    public decimal? Importe { get; set; }

    public uint? Numero { get; set; }

    public uint IdServicio { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
