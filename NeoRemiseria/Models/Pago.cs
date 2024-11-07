using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Pago
{
    // public uint Cuota { get; set; }
    public uint Id { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal? Importe { get; set; }

    // public uint IdCobro { get; set; }
    public uint IdDeuda { get; set; }

    // public virtual Cobro IdCobroNavigation { get; set; } = null!;
    public virtual Cobro IdDeudaNavigation { get; set; } = null!;
}
