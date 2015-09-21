using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    /// <summary>
    /// Controlador con una colección de widgets
    /// que serviran para el diseño del sitio
    /// TODO LO QUE SEA REPETIBLE A TRAVES DEL SITIO
    /// DEBERIA LLAMARSE DESDE AQUI
    /// </summary>
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Index()
        {
            return View();
        }
    }
}