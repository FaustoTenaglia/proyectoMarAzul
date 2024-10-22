using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;

public class DeudaService: TableService<Deuda>{
    public DeudaService (DbremiseriaContext contexto) : base(contexto) {}    
}