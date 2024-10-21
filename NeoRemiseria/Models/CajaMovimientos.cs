namespace NeoRemiseria.Models
{
    public class CajaMovimiento
    {
        public int IdMovimiento { get; set; }
        public uint Turno { get; set; }  // Relacionada con Caja
        public DateOnly Fecha { get; set; }
        public string TipoMovimiento { get; set; }  // "Ingreso" o "Egreso"
        public decimal Monto { get; set; }

        // Relación con la clase Caja
        public virtual Caja Caja { get; set; }
    }

}
