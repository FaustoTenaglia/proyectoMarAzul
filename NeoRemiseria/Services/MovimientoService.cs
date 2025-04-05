using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace NeoRemiseria.Services;
public class MovimientoService{
    protected readonly DbremiseriaContext _context; 
    // Constructor
    public MovimientoService(DbremiseriaContext contexto){
        _context = contexto;
    }

    // Metodos
    public List<Movimiento> ObtenerIngresos(Expression<Func<Movimiento, bool>>? condicion = null){
        IQueryable<Movimiento> query = _context.Movimientos.Where(m => m.IdServicioNavigation.Tipo == 1);
        if (condicion != null){
            query = query.Where(condicion);
        }
        return query.ToList();
    }
    public async Task<List<Movimiento>> ObtenerIngresosAsync(Expression<Func<Movimiento, bool>>? condicion = null){
        var servicios = _context.Servicios.Where(s => s.Tipo == 1).ToList(); // Traer todos los ingresos
        
        // Sacar los movimientos que son ingresos
        IQueryable<Movimiento> query = _context.Movimientos.Where(m => 
                                            servicios.Select(s => s.Id)
                                                     .Distinct()
                                                     .Contains(m.IdServicio ?? 0));
        // IQueryable<Movimiento> query = _context.Movimientos.Where(m => m.IdServicio == 1);
        
        // Si hay condicion de filtrado, ejecutarlo
        if (condicion != null){
            query = query.Where(condicion);
        }

        // Devolver los movimientos
        return await query.ToListAsync();
    }
    public async Task<List<Movimiento>> ObtenerGastosAsync(Expression<Func<Movimiento, bool>>? condicion = null){
        var servicios = _context.Servicios.Where(s => s.Tipo == -1).ToList(); // Traer todos los gastos
        // IQueryable<Movimiento> query = _context.Movimientos.Where(m => m.IdServicioNavigation.Tipo == -1);

        // Sacar los movimientos que son gastos
        IQueryable<Movimiento> query = _context.Movimientos.Where(m => m.IdServicio.HasValue &&
                                            servicios.Select(s => s.Id)
                                                     .Distinct()
                                                     .Contains(m.IdServicio.Value));

        if (condicion != null){
            query = query.Where(condicion);
        }

        // Devolver los movimientos
        return await query.ToListAsync();
    }
    
    public async Task<Movimiento> Registrar(Movimiento movimiento){
        try{
            // Verificar el tipo de movimiento
            var servicio = _context.Servicios.FirstOrDefault(s => s.Id == movimiento.IdServicio);
            var tipoServicio = servicio?.Tipo ?? 0; // movimiento.IdServicioNavigation.Tipo;
            if (tipoServicio*movimiento.Importe < 0){// El importe del movimiento no condice el tipo de movimiento
                // NOTA: recordar que si tipoServicio > 0, movimiento.Importe > 0
                // y que si tipoServicio < 0, movimiento.Importe < 0
                movimiento.Importe = tipoServicio < 0 ? movimiento.Importe*tipoServicio : -movimiento.Importe;
            }
            // Insertar y guardar los cambios
            _context.Add(movimiento);
            await _context.SaveChangesAsync();

            // Devolver el movimiento guardado, para poder usar el Id luego de hacer la insercion
            return movimiento;
        }
        catch(Exception e){
            throw new Exception("Error: no se pudo guardar los datos.", e);
        }
    }

    public async Task<List<Movimiento>> Obtener(Expression<Func<Movimiento, bool>>? condicion = null){
        IQueryable<Movimiento> query = _context.Movimientos;
        if (condicion != null){
            query = query.Where(condicion);
        }

        // Devolver la lista de movimientos
        return await query.ToListAsync();
    }
}
