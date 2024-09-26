using System.Linq.Expressions;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services{
    public interface IMarca{
        Task<List<Marca>> GetMarcas(Expression<Func<Marca, bool>>? predicado = null);
        Task<Marca> GetMarcaById(uint id);
        Task AddMarca(Marca marca);
        Task UpdateMarca(Marca marca);
        Task DeleteMarca(uint id);
    }
}