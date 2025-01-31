namespace NeoRemiseria.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public decimal? Importe { get; set; }

    public DateTime? Tiempo { get; set; }

    public int IdCaja { get; set; }

    public uint IdServicio { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
    public virtual Caja IdCajaNavigation { get; set; } = null!;
}
