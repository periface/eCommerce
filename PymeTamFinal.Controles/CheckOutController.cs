using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Controles
{
    public class CheckOutController : Controller
    {
        IRepositorioBase<PaypalConfig> _paypal;
        IPaypalCryptBase<PaypalConfig> _paypalEncrypService;
        public CheckOutController()
        {

        }
        public CheckOutController(IRepositorioBase<PaypalConfig> _paypal,IPaypalCryptBase<PaypalConfig>_paypalEncrypService)
        {
            this._paypal = _paypal;
            this._paypalEncrypService = _paypalEncrypService;
        }
        public paypalConfigModel activePayPalApi {
            get {
                var model = new paypalConfigModel();
                var paypal = _paypal.Cargar(a=>a.habilitada==true).SingleOrDefault();
                model.decryptedId = _paypalEncrypService.Desencriptar(paypal.appId,true);
                model.decryptedSecret = _paypalEncrypService.Desencriptar(paypal.secret,true);
                return model;
            }
        }
        public ActionResult paypalTransaccion {
            get {
                return RedirectToAction("PayPal","Comprar");
            }
        }
        public ActionResult DetalleCarro {
            get {
                return RedirectToAction("CarritoDetalle", "MiCarro", new { Area = "" }); 
            }
        }
        public ActionResult depositoInfo
        {
            get
            {
                return RedirectToAction("PayPal", "Comprar");
            }
        }
        public ActionResult otroInfo
        {
            get
            {
                return RedirectToAction("PayPal", "Comprar");
            }
        }
        public string apikey {
            get {
                return"";
            }
        }
    }
}
