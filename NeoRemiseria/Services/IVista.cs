namespace NeoRemiseria.Services;

public interface IVista<T> where T: class{
    // Interface que define una vista genérica

    // Recuperar datos
    public Task<List<T>> GetAll();

    // Filtrar datos
    public Task<List<T>> FilterBy(string campo, string valor);
}