using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Query;
using NeoRemiseria.Models;
using MySql.Data.MySqlClient;

namespace NeoRemiseria.Services;
public class ModeloService: ITable<Modelo>{
    private readonly DbremiseriaContext _context;
    // Constructor
    public ModeloService(DbremiseriaContext contexto){
        _context = contexto;
    }
    // Implementar métodos de la interface de la que deriva
    public async Task AddItem(Modelo entity){
        _context.Modelos.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Modelo>> GetAll(){
        // Devuelve todos los registros de la tabla
        // Contiene ademas el nombre de la marca a la cual pertenece el modelo
        // return await _context.Modelos.ToListAsync();
        return await _context.Modelos
            .Include(m => m.IdMarcaNavigation) // Incluye el nombre de la marca en el resultado
            .ToListAsync();
    }

    public async Task<Modelo> GetById(uint id){
        // Busca un registro, de acuerdo al id dado
        // Si no lo encunentra, devuelve un registro vacio
        return await _context.Modelos.FindAsync(id) ?? new Modelo();
    }

    public async Task UpdateItem(Modelo entity){
        try{
            // _context.Modelos.Update(entity);
            // _context.Update<Modelo>(entity);
            // Descartar entidades existentes
            Modelo entidadExistente = _context.Modelos.Find(entity.Id);
            Console.WriteLine($"Id: {entity.Id}");
            Console.WriteLine($"Nombre: {entity.Nombre}");
            Console.WriteLine($"IdMarca: {entity.IdMarca}");
            if (entidadExistente != null){
                _context.Entry(entidadExistente).State = EntityState.Detached;
            }
            // var entry = _context.Entry(entity);
            // _context.Entry<Modelo>(entity).State = EntityState.Modified;
            // Console.WriteLine($"Estado de la entidad: {entry.State}");
            
            // Establecer que la entidad fue modificada
            _context.Modelos.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            // Guardar cambios
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e){
            // Este error ocurre cuando se intenta actualizar un registro que ha sido modificado por otro usuario
            // Lanzar excepcion
            throw new Exception("Error de concurrencia: el registro a sido usado por otro usuario.", e);
        }
        catch (DbUpdateException e){
            // Este error puede ocurrir por diversos motivos, como violación de restricciones de clave foránea
            throw new Exception("Error al actualizar el registro. Verifica los datos ingresados.", e);
        }
        catch (Exception e){
            throw new Exception("Error inesperado: ocurrió un problema al intentar actualizar el registro.",e);
        }
    }

    public async Task<bool> DeleteItem(uint id){
        var modelo = await _context.Modelos.FindAsync(id);
        if (modelo == null){
            return false;
        }

        _context.Modelos.Remove(modelo);
        await _context.SaveChangesAsync();
        return true;
    }
}