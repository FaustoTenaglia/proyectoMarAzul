using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Cobro
{
    public uint Id { get; set; }

    public DateOnly? Periodo { get; set; }

    public decimal? Importe { get; set; }

    public uint? Cuotas { get; set; }

    public uint NumeroMovil { get; set; }

    public uint IdServicio { get; set; }

    public decimal Saldo {get;set;}

    public virtual Servicio IdServicioNavigation { get; set; } = null!;

    public virtual Movil NumeroMovilNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
