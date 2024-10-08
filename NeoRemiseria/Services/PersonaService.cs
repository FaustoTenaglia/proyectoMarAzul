using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class PersonaService: TableService<Persona>{
    public PersonaService(DbremiseriaContext contexto): base(contexto) { }

    public override async Task<List<Persona>> GetAll(Expression<Func<Persona, bool>>? predicado = null){
        IQueryable<Persona> query = _context.Personas;
        if (predicado != null){
            query = query.Where(predicado);
        }
        return await query.Where(p => p.Estado == 'A')
            .Include(p => p.IdLocalidadNavigation) // Incluye la localidad
            .Include(p => p.Telefonos) // Incluye los teléfonos
            .ToListAsync();
    }

    public override async Task<bool> DeleteItem(uint id){
        // Implementa la baja lógica
        // Buscar el registro por el id
        var persona = await _context.FindAsync<Persona>(id);

        // Si no se lo encuentra, devolver falso
        if (persona == null){
            return false;
        }

        var telefonos = _context.Telefonos.Where(t => t.IdPersona == id && t.Estado == 'A');

        // Borrar los teléfonos relacionados en Telefono
        // Marcar los telefónos como eliminados
        foreach (var telefono in telefonos){
            telefono.Estado = 'B';
        }
        await _context.SaveChangesAsync();

        // Baja lógica
        persona.Estado = 'B';
        await this.UpdateItem(persona);


        // No hubo problemas, devolver verdadero
        return true;
    }
}
/*
public class PersonaService: ITable<Persona>{
    private readonly DbremiseriaContext _context;
    // Constructor
    public PersonaService(DbremiseriaContext contexto){
        _context = contexto;
    }
    // Implementar métodos de la interface de la que deriva
    public async Task<Persona> AddItem(Persona entity){
        _context.Personas.Add(entity);
        await _context.SaveChangesAsync();

        return entity;
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

    public async Task<Persona?> GetById(uint id){
        return await _context.Personas.FindAsync(id);
    }

    public async Task<Persona> UpdateItem(Persona entity){
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
        
        return entity;
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
*/