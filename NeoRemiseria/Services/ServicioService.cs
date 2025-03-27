using NeoRemiseria.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NeoRemiseria.Services;
public class ServicioService: TableService<Servicio>{
    public ServicioService(DbremiseriaContext contexto): base(contexto){}
}