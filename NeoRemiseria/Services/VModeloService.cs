using System.Linq;
using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services;

public class VModeloService: IVista<VModelo>{
    private readonly DbremiseriaContext _context;

    public VModeloService(DbremiseriaContext contexto){
        _context = contexto;
    }

    public async Task<List<VModelo>> GetAll(){
        return await _context.VModelos.ToListAsync();
    }

    public async Task<List<VModelo>> FilterBy(string campo, string valor){
        // Validar el campo para evitar inyecciones SQL
        var query = _context.Set<VModelo>().AsQueryable();
        switch (campo.ToLower()){
            case "nombre":
                query = query.Where(v => v.Nombre.Equals(valor, StringComparison.OrdinalIgnoreCase));
                break;
            case "marca":
                query = query.Where(v => v.Marca.Equals(valor, StringComparison.OrdinalIgnoreCase));
                break;
            default:
                throw new ArgumentException("Campo no válido para valor de filtrado.");
        }
        return await query.ToListAsync();
    }
}