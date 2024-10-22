using NeoRemiseria.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeoRemiseria.Services;

public class PagoService
{
    private readonly DbremiseriaContext _context;

    public PagoService(DbremiseriaContext context){
            _context=context;
    }


    public async Task RegistrarPago(Pago pago){

            if(pago.Importe <= 0){
                throw new ArgumentException("No ingreso importe de pago");
            }

            var cobro= await _context.Cobros.FindAsync(pago.IdCobro);
            if(cobro != null){
                throw new ArgumentException("No se encontro la deuda asociada");
            }

            //Validar que el monto no exceda el importe total

            if(pago.Importe > cobro.Importe){
                //Revisar de tirar una exception
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            _context.Pagos.Add(pago);

            cobro.Importe -= pago.Importe;
            cobro.Cuotas -= 1;
            cobro.Saldo = cobro.Importe;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
                
        }
    }
}

   



