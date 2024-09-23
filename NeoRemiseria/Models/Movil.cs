using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Movil
{
    public uint Id { get; set; }

    public string Patente { get; set; } = null!;

    public ushort? Año { get; set; }

    public uint? IdModelo { get; set; }

    public uint? IdCartel { get; set; }

    public uint? IdTitular { get; set; }

    public virtual ICollection<Cartel> Carteles { get; set; } = new List<Cartel>();

    public virtual ICollection<Chofer> Choferes { get; set; } = new List<Chofer>();

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual Persona? IdTitularNavigation { get; set; }
}
