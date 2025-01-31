namespace NeoRemiseria.Models;

public partial class Caja
{
    public int Id {get; set;}
    public DateOnly? Jornada { get; set; }
    public decimal Entrada { get; set; }
    public decimal Salida { get; set; }
    public decimal Saldo { get; set; }
    public DateTime? Apertura {get; set;}
    public DateTime Cierre {get; set;}

    public virtual ICollection<Movimiento> Movimientos { get; set; } = [];
}
