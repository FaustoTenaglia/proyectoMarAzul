using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Marca
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
