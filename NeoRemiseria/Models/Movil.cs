using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Movil
{
    public uint Id { get; set; }

    public string Patente { get; set; } = null!;

    public int Habilitacion {get; set;} = 0;

    public int? Año { get; set; }
    // public ushort? Año { get; set; }

    public uint? IdModelo { get; set; }

    public uint? IdCartel { get; set; }

    public uint? IdTitular { get; set; }

    public char Estado {get; set;} = 'A';

    public virtual ICollection<Cartel> Carteles { get; set; } = new List<Cartel>();

    public virtual ICollection<Chofer> Choferes { get; set; } = new List<Chofer>();

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual ICollection<Deuda> Deudas { get; set; } = new List<Deuda>();

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual Persona? IdTitularNavigation { get; set; }
}
