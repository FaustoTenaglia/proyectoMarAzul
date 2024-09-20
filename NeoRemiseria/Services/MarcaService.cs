using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services{
    public class MarcaService: IMarca{
        private readonly DbremiseriaContext _context;

        //constructor
        public MarcaService(DbremiseriaContext contexto){
            _context = contexto;
        }

        //Implementar los métodos de la interface
        public async Task<List<Marca>> GetMarcas(Func<Marca, bool>? predicado = null){
            // return await _context.Marcas.ToListAsync();
            IQueryable<Marca> query = _context.Marcas;
            if (predicado != null){
                query = (IQueryable<Marca>)query.Where(predicado);
            }
            return await query.ToListAsync();
        }

        public async Task<Marca> GetMarcaById(uint id){
            // Dado un id, debe devolver la Marca
            return await _context.Marcas.FindAsync(id) ?? new Marca();
        }

        // public async Task<List<Marca>> GetMarcasOrderBy<TKey>(Func<Marca, TKey>keySelector){
        //     return await _context.Marcas.OrderBy(keySelector).ToListAsync();
        // }

        public async Task AddMarca(Marca marca){
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMarca(Marca marca){
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMarca(uint id){
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null){
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
            }
        }
    }
}