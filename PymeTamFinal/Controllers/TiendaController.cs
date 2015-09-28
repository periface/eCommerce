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
        public ActionResult Detalle(int id,string slug) {
            return View();
        }
        public ActionResult Productos(string slugCategoria,string orden,string busqueda, int? max, int? min,int? pagina=1) {
            return View();
        }
        public ActionResult Ayuda() {
            return View();
        }
    }
}