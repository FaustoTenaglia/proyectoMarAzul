namespace NeoRemiseria.Services;

public interface ITable<T> where T: class
{// Interface que define las operaciones CRUD para una tabla genérica
	// Crear nuevo registro
       Task AddItem(T entity);

       // Recuperar registros
       Task<List<T>> GetAll(); // Todos los registrs	
       Task<T> GetById(uint id); // Un registro en particular, dado el id

       // Actualizar un registro
       Task UpdateItem(T entity);

       // Eliminar un registro
       Task<bool> DeleteItem(uint id);
}