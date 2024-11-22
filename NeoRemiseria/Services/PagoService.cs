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

            // if(pago.Importe <= 0){
            //     throw new ArgumentException("No ingreso importe de pago");
            // }

            // var cobro= await _context.Cobros.FindAsync(pago.IdCobro);
            // var cobro = await _context.Deudas.FindAsync(pago.IdDeuda) ?? throw new ArgumentException("No se encontro la deuda asociada");

        //Validar que el monto no exceda el importe total

        // if (pago.Importe > cobro.Importe){
        //         //Revisar de tirar una exception
        //         throw new InvalidOperationException("El importe del pago no puede ser mayor al saldo");
        //     }
            
            // if(pago.Importe < 0){
            //     throw new ArgumentException("Ingrese un numero positivo");
            // }

            using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            _context.Pagos.Add(pago);

            // El campo saldo debería actualizarse automaticamente
            // con el trigger 'actualizar_saldo' al insertar un pago nuevo.

            // cobro.Saldo -= pago.Importe ?? 0.00m;
            // cobro.Importe -= pago.Importe;
            // cobro.Cuotas -= 1;
            // cobro.Saldo = cobro.Importe;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
                
        }
    }

    public async Task<List<Pago>> ObtenerPagos(Expression<Func<Pago, bool>>? condicion=null){
        IQueryable<Pago> query = _context.Pagos;
        if(condicion != null){
            query = query.Where(condicion);
        }

        // Devolver en una lista
        return await query.ToListAsync();
    }

    public async Task<Pago?> ObtenerUltimoPago(uint IdDeuda){
        // Obtener la última fecha de cobro
        var fecha = await _context.Pagos.Where(p => p.IdDeuda == IdDeuda)
                            .Select(p => p.Fecha)
                            .MaxAsync();
        // Verificar que se obtuvo una fecha
        if (fecha == default){ return null; }

        // Devolver el último pago
        return await _context.Pagos.Where(p => p.IdDeuda == IdDeuda && p.Fecha == fecha)
                            .FirstOrDefaultAsync();
    }
}
