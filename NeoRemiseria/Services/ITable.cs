using System.Linq.Expressions;

namespace NeoRemiseria.Services;

public interface ITable<T> where T: class
{// Interface que define las operaciones CRUD para una tabla genérica
	// Crear nuevo registro
       Task<T> AddItem(T entity);

       // Recuperar registros
       Task<List<T>> GetAll(Expression<Func<T, bool>>? predicado = null); // Todos los registros	
       Task<T?> GetById(uint id); // Un registro en particular, dado el id

       // Actualizar un registro
       Task<T> UpdateItem(T entity);

       // Eliminar un registro
       Task<bool> DeleteItem(uint id);

       // Filtrar registros por valor y campos
       Task<List<T>> FilterItems(string valor, List<string>? propiedades = null);

       // Buscar un valor en un campo dado
       Task<bool> Fetch(string valor, string campo);
}
