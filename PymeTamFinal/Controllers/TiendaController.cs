using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Productos() {
            return View();
        }
        public ActionResult Producto() {
            return View();
        }
        public ActionResult AgregarAlCarro() {
            return View();
        }
        public ActionResult EliminarDelCarro() {
            return View();
        }
    }
}