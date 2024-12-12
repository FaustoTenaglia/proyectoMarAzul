using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NeoRemiseria.Models;

public partial class Movil
{
    public uint Id { get; set; }

    [Required(ErrorMessage = "La patente es obligatoria.")]
    [RegularExpression(@"^([A-Z]{3}[0-9]{3}|[A-Z]{2}[0-9]{3}[A-Z]{2})$", ErrorMessage = "La patente debe estar en formato argentino (ej: ABC123 o AB123CD).")]
    public string Patente { get; set; } = null!;

    public int Habilitacion { get; set; } = 0;

    [Required(ErrorMessage = "El año es obligatorio.")]
    [Range(2008, 2023, ErrorMessage = "El auto no debe exceder los 15 años de antigüedad.")]
    public int? Año { get; set; }
    // public ushort? Año { get; set; }

    [Required(ErrorMessage = "El modelo es obligatorio.")]
    public uint? IdModelo { get; set; }

    public uint? IdCartel { get; set; }

    [Required(ErrorMessage = "La persona titular es obligatoria.")]
    public uint? IdTitular { get; set; }

    public char Estado { get; set; } = 'A';

    public virtual ICollection<Cartel> Carteles { get; set; } = new List<Cartel>();

    public virtual ICollection<Chofer> Choferes { get; set; } = new List<Chofer>();

    public virtual ICollection<Cobro> Cobros { get; set; } = new List<Cobro>();

    public virtual ICollection<Deuda> Deudas { get; set; } = new List<Deuda>();

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual Persona? IdTitularNavigation { get; set; }
}
