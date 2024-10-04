using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeoRemiseria.Models;

public partial class Persona
{
    public uint Id { get; set; }

    public string Dni { get; set; } = null!;

    public string? Apellido { get; set; }

    public string? Nombre { get; set; }

    public byte? Sexo { get; set; }

    public DateOnly? Nacimiento { get; set; }

    public char Estado { get; set; } = 'A';

    public string? Calle { get; set; }

    public int? Numero { get; set; }

    public uint? IdLocalidad { get; set; }

    public virtual ICollection<Chofer> Choferes { get; set; } = new List<Chofer>();

    public virtual Localidad? IdLocalidadNavigation { get; set; }

    public virtual ICollection<Movil> Moviles { get; set; } = new List<Movil>();

    public virtual ICollection<Telefono> Telefonos {get; set;} = new List<Telefono>();
}
