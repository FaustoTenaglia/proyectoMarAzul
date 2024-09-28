using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class LocalidadService: TableService<Localidad>{
    // Constructor derivado de la clase abstracta
    public LocalidadService(DbremiseriaContext contexto): base(contexto){ }
    
    // Implementar métodos de la interface de la que deriva
    public override async Task<List<Localidad>> GetAll(Expression<Func<Localidad, bool>>? predicado = null){
        IQueryable<Localidad> query = _context.Set<Localidad>();
        if (predicado != null){
            query = query.Where(predicado);
        }
        return await query
                        .Include(l => l.IdProvinciaNavigation)
                        .ToListAsync();
    }
}