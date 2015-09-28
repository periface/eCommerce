using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaWidgetsController : Controller
    {
        // GET: TiendaWidgets
        //Solo 2 temas por ahora. Eshopper y Bootstrap basico (Este ultimo podra ser modificable)
        public ActionResult MenuTienda() {
            return View("_menuTiendaEshopper");
        }
        public ActionResult SliderProductosRecomendados()
        {
            return View();
        }
        public ActionResult SliderProductosNuevos() {
            return View();
        }
        public ActionResult ListaCategorias() {
            return View();
        }
        public ActionResult ContenidoCabecera() {
            return View("_contenidoCabeceraEshopper");
        }
        public ActionResult ContenidoPiePagina() {
            return View();
        }
        //Se encarga de cargar el tema predefinido
        public ActionResult Head() {
            return View();
        }
    }
}