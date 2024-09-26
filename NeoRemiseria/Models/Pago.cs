using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Pago
{
    public int Id { get; set;}
    public uint Cuota { get; set; }


    public DateOnly Fecha { get; set; }

    public decimal? Importe { get; set; }

    public uint IdCobro { get; set; }

    public String? Descripcion { get; set;} 

    public virtual Cobro IdCobroNavigation { get; set; } = null!;
}
