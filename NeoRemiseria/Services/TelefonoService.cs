using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class TelefonoService: ITable<Telefono>{
    private readonly DbremiseriaContext _context;
    // Constructor
    public TelefonoService(DbremiseriaContext contexto){
        _context = contexto;
    }
    // Implementar métodos de la interface de la que deriva
    public async Task<Telefono> AddItem(Telefono entity){
        _context.Telefonos.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<List<Telefono>> GetAll(Expression<Func<Telefono, bool>>? predicado = null){
        IQueryable<Telefono> query = _context.Telefonos;
        if (predicado != null){
            query = query.Where(predicado);
        }
        return await query
            .Where(t => t.Estado == 'A')
            .Include(t => t.IdPersonaNavigation)
            .ToListAsync();
    }

    public async Task<Telefono> GetById(uint id){
        return await _context.Telefonos.FindAsync(id);
    }

    public async Task<Telefono> UpdateItem(Telefono entity){
        try{
            // Descartar entidades existentes
            var entidadExistente = _context.Telefonos.Find(entity.Id);
            if (entidadExistente != null){
                _context.Entry(entidadExistente).State = EntityState.Detached;
            }
            
            // Establecer que la entidad fue modificada
            _context.Telefonos.Attach(entity);
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
        // Implementar baja lógica
        // Buscar el registro por el id
        var telefono = await _context.Telefonos.FindAsync(id);
        
        // Si no lo encuentra, devolver falso
        if (telefono == null){
            return false;
        }

        // Baja lógica
        telefono.Estado = 'B';
        this.UpdateItem(telefono);

        // Devolver verdadero si no hubo problemas
        return true;
    }
}