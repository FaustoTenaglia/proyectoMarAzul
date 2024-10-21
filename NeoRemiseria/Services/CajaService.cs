namespace NeoRemiseria.Services
{
    using global::NeoRemiseria.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace NeoRemiseria.Services
    {
        public class CajaService : TableService<Caja>
        {
            private readonly DbremiseriaContext _context;

            public CajaService(DbremiseriaContext context) : base(context)
            {
                _context = context;
            }

            public async Task<Caja> GetCajaByTurnoAndFechaAsync(uint turno, DateOnly fecha)
            {
                return await _context.Cajas
                    .FirstOrDefaultAsync(c => c.Turno == turno && c.Fecha == fecha);
            }

            public async Task UpdateTotalAsync(uint turno, DateOnly fecha, decimal total)
            {
                var caja = await GetCajaByTurnoAndFechaAsync(turno, fecha);
                if (caja != null)
                {
                    caja.Total = total;
                    await _context.SaveChangesAsync();
                }
            }

            // Otros métodos específicos para Caja, si es necesario
        }
    }

}
