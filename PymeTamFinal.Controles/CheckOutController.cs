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
