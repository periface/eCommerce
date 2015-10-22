using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using Stripe;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.Stripe.Servicios
{
    //No usado
    public class StripeImplementacion : ITransaccionExternaStripeBase<stripeTarjetaModel>
    {
        public StripeImplementacion(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override string GenerarToken(stripeTarjetaModel modeloRequerido, string apiKey = "", string apiSecret = "")
        {
            var token = new StripeTokenCreateOptions()
            {
                Card = new StripeCreditCardOptions()
                {
                    AddressCountry = modeloRequerido.ciudad,
                    AddressLine1 = modeloRequerido.direccionLinea1,
                    AddressLine2 = modeloRequerido.direccionLinea2,
                    AddressCity = modeloRequerido.ciudad,
                    AddressZip = modeloRequerido.codigoPostal,
                    Cvc = modeloRequerido.cvv2,
                    AddressState = modeloRequerido.estado,
                    Number = modeloRequerido.numero,
                    ExpirationMonth = modeloRequerido.mesExp,
                    ExpirationYear = modeloRequerido.anoExp,
                    Name = modeloRequerido.nombre,
                },
            };
            var tokenServicio = new StripeTokenService(apiKey);
            var stripeToken = tokenServicio.Create(token);
            return stripeToken.Id;
        }
        public override bool EjecutarPago(string idPago, int idOrden, string idComprador = "", string api = "", string secret = "")
        {
            var orden = context.Orden.Find(idOrden);
            var cargo = new StripeChargeCreateOptions()
            {
                Currency = "MXN",
                Amount = Convert.ToInt16(orden.ordenTotal),
                Description = "Pago de Orden #" + orden.idOrden.ToString(),
                Source = new StripeSourceOptions() {
                    TokenId = idPago
                }
            };
            var chargeServicio = new StripeChargeService(api);
            
            var stripeCharge = chargeServicio.Create(cargo);
            return false;
        }
    }
}
