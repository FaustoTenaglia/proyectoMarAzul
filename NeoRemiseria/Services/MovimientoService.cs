using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;
public class MovimientoService: TableService<Movimiento>{
    public MovimientoService(DbremiseriaContext contexto): base(contexto){}
    
}