using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PreguntasFrecuentesController : Controller
    {
        IRepositorioBase<PreguntasFrecuentes> _preguntas;
        // GET: Administrador/PreguntasFrecuentes
        public ActionResult Index()
        {
            
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller,ModelState);
            return View(_preguntas.Cargar());
        }
        public ActionResult AgregarPregunta() {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarPregunta(PreguntasFrecuentes model)
        {
            if (ModelState.IsValid)
            {

                _preguntas.Agregar(model);
                ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.Agregado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            else {
                ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.ErrorBasico,ControllerContext.Controller,ModelState);
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditarPregunta() {
            return View();
        }
        public ActionResult EliminarPregunta() {
            return View();
        }
    }
}