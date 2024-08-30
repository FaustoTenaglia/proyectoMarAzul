﻿using System;
using System.Collections.Generic;

namespace NeoRemiseria.Models;

public partial class Persona
{
    public uint Id { get; set; }

    public string Dni { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Nombre { get; set; }

    public byte? Sexo { get; set; }

    public DateOnly? Nacimiento { get; set; }

    public uint? Edad { get; set; }

    public string? Calle { get; set; }

    public uint? Numero { get; set; }

    public uint? IdLocalidad { get; set; }

    public virtual ICollection<Chofer> Chofers { get; set; } = new List<Chofer>();

    public virtual Localidad? IdLocalidadNavigation { get; set; }

    public virtual ICollection<Movil> Movils { get; set; } = new List<Movil>();
}
