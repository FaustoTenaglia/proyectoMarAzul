using System;
using NeoRemiseria.Models;

namespace NeoRemiseria.Services{
    public interface IMarca{
        Task<List<Marca>> GetMarcas();
        Task<Marca> GetMarcaById(uint id);
        Task AddMarca(Marca marca);
        Task UpdateMarca(Marca marca);
        Task DeleteMarca(uint id);
    }
}