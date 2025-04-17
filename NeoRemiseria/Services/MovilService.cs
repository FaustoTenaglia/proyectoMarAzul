using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class MovilService: TableService<Movil>{
    public MovilService (DbremiseriaContext contexto): base(contexto) { }

    public override async Task<List<Movil>> GetAll(Expression<Func<Movil, bool>>? predicado=null){
        // Devuelve todos los registros de la tabla si predicado es null
        IQueryable<Movil> query = _context.Moviles;
        if (predicado != null){
            query = query.Where(predicado);
        }
        return await query
            .Include(m => m.IdTitularNavigation) // Incluir los datos de la tabla Titular
            .Include(m => m.IdModeloNavigation)  // Incluir los datos de la tabla Modelo
            .ToListAsync();
    }

    public override async Task<bool> DeleteItem(uint id){
        var movil = _context.Moviles.FirstOrDefault(m => m.Id == id);
        if (movil != null){
            movil.Choferes = _context.Choferes.Where(c => c.NumeroMovil == movil.Id)
                                              .ToList();
            if (movil.Choferes.Count != 0){
                foreach(var chofer in movil.Choferes){
                    if (chofer != null){
                        _context.Entry(chofer).State = EntityState.Detached;
                        chofer.FechaHasta = DateOnly.FromDateTime(DateTime.Now);
                        chofer.Observacion = "Móvil fue dado de baja.";
                        chofer.Estado = 'B';

                        // Establecer que el chofer fue modificado en Choferes
                        _context.Choferes.Attach(chofer);
                        _context.Entry(chofer).State = EntityState.Modified;
                    }
                }
            }

            _context.Entry(movil).State = EntityState.Detached;

            // Actualizar el movil
            movil.Estado = 'B';

            // Establecer que fue modificado
            _context.Moviles.Attach(movil);
            _context.Entry(movil).State = EntityState.Modified;

            // Guardar los cambios
            await _context.SaveChangesAsync();
            return true;
        }
        else{
            return false;
        }
    }
}
/*
{
    public class MovilService: ITable<Movil>{
        private readonly DbremiseriaContext _context;

        public MovilService(DbremiseriaContext contexto){
            _context = contexto;
        }

        // Implementar métodos de la interface de la que deriva
        public async Task<Movil> AddItem(Movil entity){
            _context.Moviles.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Movil>> GetAll(Expression<Func<Movil, bool>>? predicado=null){
            // Devuelve todos los registros de la tabla si predicado es null
            IQueryable<Movil> query = _context.Moviles;
            if (predicado != null){
                query = query.Where(predicado);
            }
            return await query
                .Include(m => m.IdModeloNavigation)
                .Include(m => m.IdTitularNavigation)
                .ToListAsync();
        }

        public async Task<Movil> GetById(uint id){
            // Busca un registro, de acuerdo al id dado
            // Si no lo encunentra, devuelve null
            return await _context.Moviles.FindAsync(id);
        }

        public async Task<Movil> UpdateItem(Movil entity){
            try{
                Movil entidadExistente = _context.Moviles.Find(entity.Id);
                if (entidadExistente != null){
                    _context.Entry(entidadExistente).State = EntityState.Detached;
                }
                
                // Establecer que la entidad fue modificada
                _context.Moviles.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                // Guardar cambios
                await _context.SaveChangesAsync();

                return entity;
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
            var movil = await _context.Moviles.FindAsync(id);
            if (movil == null){
                return false;
            }

            _context.Moviles.Remove(movil);
            await _context.SaveChangesAsync();
            return true;
        }
    }   
}
*/