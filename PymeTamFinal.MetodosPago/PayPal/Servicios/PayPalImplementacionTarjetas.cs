using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.PayPal.Servicios
{
    public class PayPalImplementacionTarjetas : ITransaccionExternaPayPalBase<payPalTarjetaModel>
    {
        public PayPalImplementacionTarjetas(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
