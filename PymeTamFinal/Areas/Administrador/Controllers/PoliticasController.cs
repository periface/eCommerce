using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PoliticasController : AdminController
    {
        IRepositorioBase<Politicas> _politicas;
        public PoliticasController(IRepositorioBase<Politicas> _politicas)
        {
            this._politicas = _politicas;
        }
        // GET: Administrador/Politicas
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            return View(_politicas.Cargar());
        }
        public ActionResult NuevaPolitica()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller, ModelState);
            return View();
        }
        [HttpPost]
        public ActionResult NuevaPolitica([Bind(Include = "contenidoPolitica,nombrePolitica")]Politicas model)
        {
            if (ModelState.IsValid)
            {
                model.fechaActualizacion = DateTime.Now;
                model.fechaPublicacion = DateTime.Now;
                _politicas.Agregar(model);
                ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.Agregado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.ErrorValidacion, ControllerContext.Controller, ModelState);
            return RedirectToAction("Index");
        }
        public ActionResult EditarPolitica(int? id)
        {
            ViewBag.parcial = true;
            if (id.HasValue)
            {
                var politica = _politicas.CargarPorId(id);

                if (Request.IsAjaxRequest())
                {
                    if (politica == null)
                    {
                        return error404Parcial;
                    }
                    return View(politica);
                }
                ViewBag.parcial = false;
                return View(politica);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult EditarPolitica([Bind(Include = "contenidoPolitica,nombrePolitica,fechaPublicacion,idPolitica")] Politicas model)
        {
            ViewBag.parcial = false;
            if (ModelState.IsValid)
            {
                model.fechaActualizacion = DateTime.Now;
                _politicas.Editar(model);
                ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.Editado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EliminarPolitica(int id)
        {
            var politica = _politicas.CargarPorId(id);
            if (politica == null)
            {
                return error404Parcial;
            }
            else {
                return View(politica);
            }
        }
        [HttpPost]
        public ActionResult EliminarPolitica(Politicas model)
        {
            _politicas.Eliminar(model);
            ServicioDeMensajes.darMensaje(HtmlHelpers.Abstraccion.enumMensaje.Eliminado,ControllerContext.Controller);
            return RedirectToAction("Index");
        }
    }
}