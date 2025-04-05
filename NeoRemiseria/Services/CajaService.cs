using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services;
public class CajaService{
    protected readonly DbremiseriaContext _context; // Atributo privado
    // Indica la caja con la que se está trabajando
    protected private int? _idCajaActual;
    protected private bool _estaAbierta = false;
    
    // Constructor
    public CajaService(DbremiseriaContext contexto){
        _context = contexto;
        var caja = _context.Cajas.Any() ? _context.Cajas.OrderBy(c => c.Id).LastOrDefault(c => !c.Cierre.HasValue) : null;
        if (caja != null){
            _idCajaActual = caja.Id;
            _estaAbierta = true;
        }
    }

    // Metodos
    public async Task<Caja?> BuscarCajaAbierta(){
        // Busca y devuelve la primer caja que se encuentre abierta.
        // Si no existe ninguna, devuelve un valor nulo
        var caja = _context.Cajas.Any() ? await _context.Cajas.FirstOrDefaultAsync(c => !c.Cierre.HasValue) : null;
        return caja;
    }

    // Set
    public async Task EstablecerIdCajaActual(int? idCaja = null){
        // Establece el id de la caja actual con la que se está trabajando
        // Por defecto, establece a la primera caja abierta que encuentre
        if (idCaja != null){
            _idCajaActual = idCaja;
            _estaAbierta = _context.Cajas.Where(c => c.Id == idCaja).Any(c => !c.Cierre.HasValue);
        }else{
            var caja = await BuscarCajaAbierta();
            if (caja != null){
                _idCajaActual = caja.Id;
                _estaAbierta = true;
            }
        }
    }

    // Get
    public int? IdCajaActual(){
        // Devuelve el id de la caja con la que se encuentra actualmente trabajando
        return _idCajaActual;
    }
    public async Task<bool> EstaAbierta(int? idCaja = null){
        // Dado un id de caja, devuelve el estado de la misma (abierta o cerrada)
        if (idCaja != null){
            Caja? caja = await _context.Cajas.FirstOrDefaultAsync(c => c.Id == idCaja);
            _estaAbierta = caja != null && !caja.Cierre.HasValue;
        }
        return _estaAbierta;
    }

    public async Task<Caja> AbrirCaja(){
        try{
            // Verificar que no exista una caja abierta
            // var cajaAbierta = BuscarCajaAbierta();
            if (_idCajaActual.HasValue && _estaAbierta){
                throw new Exception($"Existe una instancia de caja abierta, id = {_idCajaActual}.");
            }

            // Todas las cajas cerradas, seguir
            Caja caja = new Caja
            {
                Apertura = DateTime.Now,
                Jornada = DateOnly.FromDateTime(DateTime.Now)
            };

            // Insertar registro y guardar
            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();

            // Devolver la entidad guardada para poder usar el nuevo id
            _idCajaActual = caja.Id;
            _estaAbierta = true;
            return caja;
        }
        finally {}
    }

    public void CerrarCaja(int? id = null){
        // Buscar el registro en la tabla
        Caja? caja = (id == null) ? _context.Cajas.Find(_idCajaActual) : _context.Cajas.Find(id);
        if (caja != null && !caja.Cierre.HasValue){
            // Cerrar caja y guardar en la base
            _context.Entry(caja).State = EntityState.Detached;
            caja.Cierre = DateTime.Now;
            _context.Attach(caja);
            _context.Entry(caja).State = EntityState.Modified;
        
            // Guardar cambios
            _context.SaveChanges();
            _idCajaActual = null; // NOTA: Analizar si está es la mejor opción
            _estaAbierta = false;
        }
    }

    public void CargarMovimientosCaja(int? id = null, Expression<Func<Caja, bool>>? predicado = null){
        // TODO: investigar si es necesario asignar los valores de los movimientos para cada caja
        Caja? caja = (id == null) ? _context.Cajas.FirstOrDefault(c => c.Id == _idCajaActual) : _context.Cajas.FirstOrDefault(c => c.Id == id);
        if (caja != null){
            caja.Movimientos = [.. _context.Movimientos.Where(m => m.IdCaja == id)];
        }
    }

    public async Task<List<Caja>?> ObtenerCajasAbiertas(){
        // Devuelve una lista de caja que se encuentren abiertas
        // NOTE: es preferible una lista de elementos Caja o un listado con sólo los id?
        return await _context.Cajas.Where(c => !c.Cierre.HasValue).ToListAsync() ?? null;
    }
    public async Task<List<Caja>?> ObtenerCajasCerradas(){
        // Devuelve una lista de caja que se encuentren abiertas
        // NOTE: es preferible una lista de elementos Caja o un listado con sólo los id?
        return await _context.Cajas.Where(c => c.Cierre.HasValue).ToListAsync() ?? null;
    }
    
    public async Task<Caja?> ObtenerCaja(int? id = null){
        // Obtiene una caja específica, dado el id
        // Por defecto, devuelve el valor de _idCajaActual
        if (id != null){
            return await _context.Cajas.FirstOrDefaultAsync(c => c.Id == id);
        }else{
            return await _context.Cajas.FirstOrDefaultAsync(c => c.Id == _idCajaActual);
        }
    }

    public async Task<Caja?> ActualizarCaja(int? idCaja = null){
        // Actualiza los valores desde la base de datos de una caja dada
        // Este método debe llamarse luego de hacer una inserción en la tabla Movimiento,
        // para que refleje los cambios que realiza el trigger en la base de datos.
        
        _context.ChangeTracker.Clear();
        if (idCaja == null){
            return await ObtenerCaja(); // Devolver la caja actual
        }else{
            return await ObtenerCaja(idCaja); // Devolver la caja especificada por `idCaja`
        }
    } 
}