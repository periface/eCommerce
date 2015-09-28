using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaController : Controller
    {
        // Inicio de la tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetalleProducto(int id,string slug) {
            return View();
        }
        public ActionResult Productos(int? idCategoria, int? pagina, string orden, string busquedaString, string busqueda, int? min, int? max) {

            return View();
        }
        public ActionResult Ayuda() {
            return View();
        }
    }
}