using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Deuda{
    public uint Id {get; set;}
    public DateOnly Periodo {get; set;}
    public decimal Importe {get; set;}
    public decimal? Saldo {get; set;}
    public uint IdMovil {get; set;}

    public virtual Movil? IdMovilNavigation {get; set;}

    public virtual ICollection<Movimiento> Movimientos {get; set;} = new List<Movimiento>();
}