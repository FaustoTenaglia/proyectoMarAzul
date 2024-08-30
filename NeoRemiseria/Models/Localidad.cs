using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Localidad
{
    public uint Id { get; set; }

    public string? Nombre { get; set; }

    public byte? IdProvincia { get; set; }

    public virtual Provincium? IdProvinciaNavigation { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
