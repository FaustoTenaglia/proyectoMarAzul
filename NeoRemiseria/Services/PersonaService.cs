using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class PersonaService: ITable<Persona>{
    private readonly DbremiseriaContext _context;
    // Constructor
    public PersonaService(DbremiseriaContext contexto){
        _context = contexto;
    }
    // Implementar métodos de la interface de la que deriva
    public async Task AddItem(Persona entity){
        _context.Personas.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Persona>> GetAll(Expression<Func<Persona, bool>>? predicado = null){
        IQueryable<Persona> query = _context.Personas;
        if (predicado != null){
            query = query.Where(predicado);
        }
        return await query
            .Include(p => p.IdLocalidadNavigation)
            .ToListAsync();
    }

    public async Task<Persona> GetById(uint id){
        return await _context.Personas.FindAsync(id) ?? new Persona();
    }

    public async Task UpdateItem(Persona entity){
        try{
            // Descartar entidades existentes
            var entidadExistente = _context.Personas.Find(entity.Id);
            if (entidadExistente != null){
                _context.Entry(entidadExistente).State = EntityState.Detached;
            }
            
            // Establecer que la entidad fue modificada
            _context.Personas.Attach(entity);
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
        var Persona = await _context.Personas.FindAsync(id);
        if (Persona == null){
            return false;
        }

        _context.Personas.Remove(Persona);
        await _context.SaveChangesAsync();
        return true;
    }
}