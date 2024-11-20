using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;

namespace NeoRemiseria.Services;

public class GastoService
{
    private readonly DbremiseriaContext _context;

    public GastoService(DbremiseriaContext context)
    {
        _context = context;
    }

    public async Task RegistrarGasto(Gasto gasto)
    {
        if (gasto.Importe <= 0)
        {
            throw new ArgumentException("No ingresó importe de gasto");
        }

        // Validar que el servicio asociado exista
        var servicio = await _context.Servicios.FindAsync(gasto.IdServicio);
        if (servicio == null)
        {
            throw new ArgumentException("No se encontró el servicio asociado");
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            _context.Gastos.Add(gasto);

            

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }
}


