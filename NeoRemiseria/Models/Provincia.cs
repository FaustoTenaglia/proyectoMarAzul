using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Provincia
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Localidad> Localidads { get; set; } = new List<Localidad>();
}
