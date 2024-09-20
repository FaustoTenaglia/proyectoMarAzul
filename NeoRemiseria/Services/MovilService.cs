using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace NeoRemiseria.Services
{
    public class MovilService: ITable<Movil>{
        private readonly DbremiseriaContext _context;

        public MovilService(DbremiseriaContext contexto){
            _context = contexto;
        }

        // Implementar métodos de la interface de la que deriva
        public async Task AddItem(Movil entity){
            _context.Movils.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movil>> GetAll(Func<Movil, bool>? predicado=null){
            // Devuelve todos los registros de la tabla si predicado es null
            IQueryable<Movil> query = _context.Movils;
            if (predicado != null){
                query = (IQueryable<Movil>) query.Where(predicado);
            }
            return await query
                .Include(m => m.IdModeloNavigation)
                .Include(m => m.IdTitularNavigation)
                .ToListAsync();
        }

        public async Task<Movil> GetById(uint id){
            // Busca un registro, de acuerdo al id dado
            // Si no lo encunentra, devuelve un registro vacio
            return await _context.Movils.FindAsync(id) ?? new Movil();
        }

        public async Task UpdateItem(Movil entity){
            try{
                Movil entidadExistente = _context.Movils.Find(entity.Id);
                if (entidadExistente != null){
                    _context.Entry(entidadExistente).State = EntityState.Detached;
                }
                
                // Establecer que la entidad fue modificada
                _context.Movils.Attach(entity);
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
            var movil = await _context.Movils.FindAsync(id);
            if (movil == null){
                return false;
            }

            _context.Movils.Remove(movil);
            await _context.SaveChangesAsync();
            return true;
        }
    }   
}