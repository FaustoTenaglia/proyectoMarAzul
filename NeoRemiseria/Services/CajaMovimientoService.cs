using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services
{
    public class CajaMovimientoService : TableService<CajaMovimiento>
    {
        private readonly DbremiseriaContext _context;

        public CajaMovimientoService(DbremiseriaContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<CajaMovimiento> GetMovimientosByTurno(uint turno)
        {
            return _context.CajaMovimientos
                .Where(cm => cm.Turno == turno)
                .ToList();
        }

        public CajaMovimiento GetMovimientoById(int idMovimiento)
        {
            return _context.CajaMovimientos
                .FirstOrDefault(cm => cm.IdMovimiento == idMovimiento);
        }

        public void UpdateMovimiento(CajaMovimiento movimiento)
        {
            _context.CajaMovimientos.Update(movimiento);
            _context.SaveChanges();
        }

        public void DeleteMovimiento(int idMovimiento)
        {
            var movimiento = GetMovimientoById(idMovimiento);
            if (movimiento != null)
            {
                _context.CajaMovimientos.Remove(movimiento);
                _context.SaveChanges();
            }
        }

        // Otros métodos específicos para CajaMovimiento, si es necesario
    }

}
