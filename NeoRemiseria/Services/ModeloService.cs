using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;

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
        return await _context.Modelos.ToListAsync();
    }

    public async Task<Modelo> GetById(uint id){
        return await _context.Modelos.FindAsync(id);
    }

    public async Task UpdateItem(Modelo entity){
        _context.Modelos.Update(entity);
        await _context.SaveChangesAsync();
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