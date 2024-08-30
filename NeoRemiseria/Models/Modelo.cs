using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Modelo
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public uint? IdMarca { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<Movil> Movils { get; set; } = new List<Movil>();
}
