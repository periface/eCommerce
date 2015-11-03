using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.MetodosPago.PayPal;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PayPalController : AdminController
    {
        IPaypalCryptBase<PaypalConfig> _config;
        ITransaccionExterna<PaypalPagoClienteModel> _paypal;
        public PayPalController(IPaypalCryptBase<PaypalConfig> _config, ITransaccionExterna<PaypalPagoClienteModel> _paypal)
        {
            this._config = _config;
            this._paypal = _paypal;
        }
        // GET: Administrador/PayPal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agregar() {
            
            return View();
        }
        [HttpPost]
        public ActionResult Agregar(PaypalModel model) {
            var dbPaypal = new PaypalConfig()
            {
                appId = _config.Encriptar(model.appId, true),
                secret = _config.Encriptar(model.secret, true),
                emailCuenta = model.emailCuenta,
                habilitada = model.habilitada
            };
            _config.Guardar(dbPaypal);
            return View();
        }
        public ActionResult ComprobarConexion(string key,string secret) {
            string error;
            var estado = _paypal.ComprobarConexion(key,secret,out error);
            return Json(new { estado = estado,error = error  },JsonRequestBehavior.AllowGet);
        }
    }
}