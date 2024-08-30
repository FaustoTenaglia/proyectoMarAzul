using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Servicio
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public sbyte? Tipo { get; set; }

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
