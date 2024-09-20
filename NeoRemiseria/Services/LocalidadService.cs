using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services;
public class LocalidadService: ITable<Localidad>{
    private readonly DbremiseriaContext _context;
    // Constructor
    public LocalidadService(DbremiseriaContext contexto){
        _context = contexto;
    }
    // Implementar métodos de la interface de la que deriva
    public async Task AddItem(Localidad entity){
        _context.Localidades.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Localidad>> GetAll(Func<Localidad, bool>? predicado = null){
        IQueryable<Localidad> query = _context.Localidades;
        if (predicado != null){
            query = (IQueryable<Localidad>)query.Where(predicado);
        }
        return await query.ToListAsync();
    }

    public async Task<Localidad> GetById(uint id){
        return await _context.Localidades.FindAsync(id) ?? new Localidad();
    }

    public async Task UpdateItem(Localidad entity){
        try{
            // Descartar entidades existentes
            var entidadExistente = _context.Localidades.Find(entity.Id);
            if (entidadExistente != null){
                _context.Entry(entidadExistente).State = EntityState.Detached;
            }
            
            // Establecer que la entidad fue modificada
            _context.Localidades.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            // Guardar cambios
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e){
            Console.WriteLine("Error de concurrencia: " + e.Message);
        }
        catch(Exception e){
            Console.WriteLine("Error al actualizar: " + e.Message);
        }
    }

    public async Task<bool> DeleteItem(uint id){
        var Localidad = await _context.Localidades.FindAsync(id);
        if (Localidad == null){
            return false;
        }

        _context.Localidades.Remove(Localidad);
        await _context.SaveChangesAsync();
        return true;
    }
}