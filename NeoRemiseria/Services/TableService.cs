using NeoRemiseria.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore.MySql.Query;

namespace NeoRemiseria.Services;
// Clase abstracta para implementar la interface ITable
public abstract class TableService<T>: ITable<T> where T: class{
    protected readonly DbremiseriaContext _context;

    // Constructor
    public TableService(DbremiseriaContext contexto){
        _context = contexto;
    }

    // Implementar métodos
    // Create
    public virtual async Task<T> AddItem(T entity){
        // Insertar registro
        _context.Add<T>(entity);

        // Guardar cambios en la base
        await _context.SaveChangesAsync();

        // Devolver la entidad guardada
        return entity; // Para poder usar el Id que se genera al insertar
    }

    // Recovery
    public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>>? predicado = null){
        IQueryable<T> query  = _context.Set<T>();
        // Preguntar por la condicion
        if (predicado != null){
            query = query.Where(predicado);
        }

        // Devolver en una lista
        return await query.ToListAsync();
    }
    public virtual async Task<T> GetById(uint id){
        return await _context.FindAsync<T>(id); // buscar por el id
    }

    // Update
    public virtual async Task<T> UpdateItem(T entity){
        try{
            uint id = (uint)typeof(T).GetProperty("Id").GetValue(entity);
            T entidadExistente = _context.Find<T>(id);
            if (entidadExistente != null){
                _context.Entry(entidadExistente).State = EntityState.Detached;
            }

            // Establecer que la entidad fue modificada
            _context.Attach<T>(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;

            // Guardar cambios
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e){
            // Este error ocurre cuando se intenta actualizar un registro que ha sido modificado por otro usuario
            throw new Exception("Error de concurrencia: el registro a sido usado por otro usuario.", e);
        }
        catch (DbUpdateException e){
            // Este error puede ocurrir por diversos motivos, como violación de restricciones de clave foránea
            throw new Exception("Error al actualizar el registro. Verifica los datos ingresados.", e);
        }
        catch (Exception e){
            throw new Exception("Error inesperado: ocurrió un problema al intentar actualizar el registro.",e);
        }

        // Devolver la entidad
        return entity;
    }

    // Delete
    public virtual async Task<bool> DeleteItem (uint id){
        var entity = await _context.FindAsync<T>(id);
        if (entity == null){
            return false;
        }
        // Eliminar
        _context.Remove<T>(entity);

        // Guardar cambios
        await _context.SaveChangesAsync();

        // Devolver verdadero
        return true;
    }
}

