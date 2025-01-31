using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data;

namespace NeoRemiseria.Services;
public class CajaService{
    protected readonly DbremiseriaContext _context; // Atributo privado
    
    // Constructor
    public CajaService(DbremiseriaContext contexto){
        _context = contexto;
    }

    // Metodos
    public async Task<Caja> BuscarCajaAbierta(){
        return await _context.Cajas.FirstAsync(c => c.Cierre == default);
    }
    public async Task<Caja> AbrirCaja(){
        try{
            // Verificar que no exista una caja abierta
            var cajaAbierta = BuscarCajaAbierta();
            if (cajaAbierta != null){
                throw new Exception("Existe una instancia de caja abierta.");
            }

            // Todas las cajas cerradas, seguir
            Caja caja = new Caja
            {
                Apertura = DateTime.Now,
                Jornada = DateOnly.FromDateTime(DateTime.Now),
                Cierre = default
            };

            // Insertar registro y guardar
            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();

            // Devolver la entidad guardada para poder usar el nuevo id
            return caja;
        }
        finally {}
    }

    public void CerrarCaja(int id){
        // Buscar el registro en la tabla
        var caja = _context.Cajas.Find(id);
        if (caja != null){
            // Cerrar caja y guardar en la base
            _context.Entry(caja).State = EntityState.Detached;
            caja.Cierre = DateTime.Now;
            _context.Attach(caja);
            _context.Entry(caja).State = EntityState.Modified;
        
            // Guardar cambios
            _context.SaveChanges();
        }
    }
    public void CargarMovimientosCaja(int id, Expression<Func<Caja, bool>>? predicado = null){
        // TODO: investigar si es necesario asignar los valores de los movimientos para cada caja
        var caja = _context.Cajas.FirstOrDefault(c => c.Id == id);
        if (caja != null){
            caja.Movimientos = [.. _context.Movimientos.Where(m => m.IdCaja == id)];
        }
    }
}