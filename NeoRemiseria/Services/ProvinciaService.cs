using Microsoft.EntityFrameworkCore;
using NeoRemiseria.Models;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class ProvinciaService: TableService<Provincia>{
    // Clase derivada de la clase abstracta TableService
    // Constructor
    public ProvinciaService(DbremiseriaContext contexto): base(contexto){}

    // Aquí se puede redefinir otro métodos...
}