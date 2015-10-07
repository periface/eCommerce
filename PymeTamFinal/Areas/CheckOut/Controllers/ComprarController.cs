using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Controllers
{
    public class ComprarController : Controller
    {
        // GET: CheckOut/Comprar
        public ActionResult MetodoPago(string metodoPago)
        {
            return View();
        }
        public ActionResult InvocaPago() {
            Pagar("","","","");
            return View();
        }
        //Solo si el metodo es electronico
        public void Pagar(params string[] tokens) {
            foreach (var token in tokens)
            {

            }
        }
    }
}