using PymeTamFinal.Modelos.ModelosAuxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.PayPal.Servicios
{
    public class PayPalImplementacionCliente : ITransaccionExternaPayPalBase<paypalPagoClienteModel>
    {
        public override string GenerarToken(paypalPagoClienteModel modeloRequerido, string apiKey, string apiSecret)
        {
            return base.GenerarToken(modeloRequerido,apiKey,apiSecret);
        }
        public override bool EjecutarPago(int idPago, int idOrden, int idComprador)
        {
            return base.EjecutarPago(idPago, idOrden, idComprador);
        }
    }
}
