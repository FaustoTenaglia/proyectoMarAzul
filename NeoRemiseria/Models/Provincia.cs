using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Provincia
{
    public uint Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Localidad> Localidades { get; set; } = new List<Localidad>();
}
