using PayPal.Api;
using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace PymeTamFinal.MetodosPago.PayPal.Servicios
{
    public class PayPalImplementacionServidor : ITransaccionExternaPayPalBase<paypalPagoClienteModel>
    {
        public PayPalImplementacionServidor(DataContext context):base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override string GenerarToken(paypalPagoClienteModel modeloRequerido, string apiKey, string apiSecret)
        {
            //Muchas responsabilidades
            return crearPago(modeloRequerido,apiKey,apiSecret);
        }
        private string crearPago(paypalPagoClienteModel modeloRequerido, string apiKey, string apiSecret)
        {
            var ctx = (APIContext)GenerarContexto(apiKey, apiSecret);
            var items = new ItemList();
            items.items = new List<Item>();
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            var redUrl = new RedirectUrls()
            {
                cancel_url = modeloRequerido.cancelUrl,
                return_url = modeloRequerido.returnUrl,
            };
            var infoTrans = detallesGenerador(modeloRequerido.pedido, modeloRequerido.tipoMoneda);
            items.items = infoTrans.items;
            var transaccionList = new List<Transaction>() {
             new Transaction() {
                 description = "Pago de pedido. ",
                 amount = infoTrans.monto,
                 invoice_number = new Random().Next(999999).ToString(),
                 item_list = items
             }
            };
            Payment pymnt = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transaccionList,
                redirect_urls = redUrl,
            };
            var pagoCreado = pymnt.Create(ctx);
            return pagoCreado.token;
        }

        private infoTransaccion detallesGenerador(int idOrden, string crncy)
        {
            var info = new infoTransaccion();
            var orden = context.Orden.Include("ordenDetalle").SingleOrDefault(a => a.idOrden == idOrden);
            var detalles = new Details()
            {
                subtotal = orden.ordenTotal.ToString("#.##"),

            };
            info.detalles = detalles;
            var montos = new Amount()
            {
                currency = crncy,
                total = orden.ordenTotal.ToString("#.##"),
            };
            info.items.Add(new Item() {
                currency = crncy,
                name = "Mi Compra",
                price = orden.ordenTotal.ToString("#.##"),
                quantity = "1",
            });
            info.monto = montos;
            return info;
        }
        private class infoTransaccion
        {
            public infoTransaccion()
            {
                detalles = new Details();
                monto = new Amount();
                items = new List<Item>();
            }
            public Details detalles { get; set; }
            public Amount monto { get; set; }
            public List<Item> items { get; set; }
        }
        public override bool ComprobarConexion(string apiKey, string apiSecret, out string error)
        {

            error = string.Empty;
            try
            {
                var opciones = new Dictionary<string, string>();
                opciones.Add("mode", "sandbox");
                string tokenAcceso = new OAuthTokenCredential(apiKey, apiSecret, opciones).GetAccessToken();
                if (string.IsNullOrEmpty(tokenAcceso))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public override object GenerarContexto(string api, string secret)
        {
            var opciones = new Dictionary<string, string>();
            opciones.Add("mode", "sandbox");
            string tokenAcceso = new OAuthTokenCredential(api, secret, opciones).GetAccessToken();
            APIContext apiContext = new APIContext(tokenAcceso);
            return apiContext;
        }
        public override bool EjecutarPago(string idPago, int idOrden, string idComprador, string api, string secret)
        {
            var apiContext = (APIContext)GenerarContexto(api,secret);
            var pago = Payment.Get(apiContext,idPago);
            var ejecucion = new PaymentExecution() {
                payer_id = idComprador
            };
            var pagoEjecutado = pago.Execute(apiContext,ejecucion);
            if (pagoEjecutado.state == "approved")
            {
                actualizarOrden(idPago, idOrden,pagoEjecutado.create_time);
                return true;
            }
            else {
                return false;
            }
        }

        private void actualizarOrden(string idPago,int idOrden,string fecha)
        {
            var orden = context.Orden.Find(idOrden);
            orden.ordenFechaPago=Convert.ToDateTime(fecha);
            orden.ordenPagado = true;
            orden.ordenRevisado = false;
            orden.ordenCodigoPayPal = idPago;
            orden.ordenEstadoPedido = "Pagado - Pendiente de envió";
            context.Entry(orden).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
