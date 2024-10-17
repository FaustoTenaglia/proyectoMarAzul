using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;

public class ChoferService: TableService<Chofer>{

    public ChoferService (DbremiseriaContext contexto): base(contexto) {}

    public override async Task<List<Chofer>> GetAll (Expression<Func<Chofer, bool>>? predicado = null){
        IQueryable<Chofer> query = _context.Choferes;
        if (predicado != null){
            query = query.Where(predicado);
        }

        // Devolver resultado
        return await query.Where(c => c.Estado == 'A')
            .Include(c => c.IdPersonaNavigation)
            .Include(c => c.NumeroMovilNavigation)
            .ToListAsync();
    }
}