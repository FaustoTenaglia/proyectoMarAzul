using NeoRemiseria.Models;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

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
    public virtual async Task<T?> GetById(uint id){
        return await _context.FindAsync<T>(id); // buscar por el id
    }

    // Update
    public virtual async Task<T> UpdateItem(T entity){
        try{
            // var id = (uint)typeof(T).GetProperty("Id").GetValue(entity);
            // Obtener la propiedad "Id" de la entidad
            var propiedadInfo = typeof(T).GetProperty("Id"); // Uso de reflexión
            if (propiedadInfo == null){
                throw new InvalidOperationException("La entidad no tiene una propiedad Id.");
            }

            // Obtener el valor del Id
            var idValor = propiedadInfo.GetValue(entity);
            if (idValor == null){
                throw new InvalidOperationException("El Id no puede ser nulo.");
            }

            // Asegurar que es un valor entero sin signo
            uint id = Convert.ToUInt32(idValor); // Si no es posible la conversión, lanza una excepción

            var entidadExistente = await _context.FindAsync<T>(id);
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

    // Filter
    public virtual async Task<List<T>> FilterItems(string valor, List<string>? properties = null){
        
        var query = _context.Set<T>().AsQueryable();

        // Si no se especifican propiedades, obtenemos todas las propiedades excepto "Id"
        if (properties == null || !properties.Any()){
            properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .Where(p => p.Name != "Id")
                                   .Select(p => p.Name)
                                   .ToList();
        }

        foreach (var property in properties){
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(parameter, property);
            var constant = Expression.Constant(valor);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            if (containsMethod != null){
                var containsExpression = Expression.Call(member, containsMethod, constant);
                var predicate = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
                query = query.Where(predicate);
            }
        }

        return await query.ToListAsync();
    }
}

