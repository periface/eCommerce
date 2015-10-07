using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class CostosEnvioController : AdminController
    {
        IRepositorioBase<CostosEnvio> _envios;
        IRepositorioBase<Estados> _estados;
        public CostosEnvioController(IRepositorioBase<CostosEnvio> _envios, IRepositorioBase<Estados> _estados)
        {
            this._estados = _estados;
            this._envios = _envios;
        }
        // GET: Administrador/CostosEnvio
        public ActionResult Index()
        {
            var model = _envios.Cargar();
            return View(model);
        }
        public ActionResult CargaEnviosCAjax() {
            var model = _envios.Cargar();
            return View(model);
        }
        public ActionResult AgregarEnvio()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarEnvio(CostosEnvio model) {
            if (Request.IsAjaxRequest()) {
                if (ModelState.IsValid)
                {
                    _envios.Agregar(model);
                    return Json(jsonResult(ModelState, true, model), JsonRequestBehavior.AllowGet);
                }
                else {
                    ModeloJson json = new ModeloJson();
                    return Json(jsonResult(ModelState, null, model), JsonRequestBehavior.AllowGet);
                }
            }
            return HttpNotFound();
        }
        public ActionResult VerDetalle(int? id)
        {
            if (Request.IsAjaxRequest())
            {

                if (!id.HasValue)
                {
                    return error404Parcial;
                }
                var model = new DetalleEnvioViewModel();
                model.costoEnvio = _envios.CargarPorId(id);
                if (model.costoEnvio == null)
                    return error404Parcial;
                model.estados = _estados.Cargar(a => a.costosEnvio.Any(e => e.idEnvio == id.Value));
                return View(model);
            }
            else {
                var model = new DetalleEnvioViewModel();
                model.costoEnvio = _envios.CargarPorId(id);
                if (model.costoEnvio == null)
                    return error404Parcial;
                model.estados = _estados.Cargar(a => a.costosEnvio.Any(e => e.idEnvio == id.Value));
                return View(model);
            }
        }
        public ActionResult EditarEnvio(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (!id.HasValue)
                    return error404Parcial;
                var envio = _envios.CargarPorId(id);
                if (envio == null)
                    return error404Parcial;
                return View(envio);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditarEnvio(CostosEnvio model) {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    _envios.Editar(model);
                    return Json(jsonResult(ModelState, true, model), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModeloJson json = new ModeloJson();
                    return Json(jsonResult(ModelState, null, model), JsonRequestBehavior.AllowGet);
                }
            }
            return HttpNotFound();
        }
        public ActionResult EliminarEnvio(int? id)
        {
            if (Request.IsAjaxRequest())
            {

                if (!id.HasValue)
                {
                    return error404Parcial;
                }
                var model = new DetalleEnvioViewModel();
                model.costoEnvio = _envios.CargarPorId(id);
                if (model.costoEnvio == null)
                    return error404Parcial;
                model.estados = _estados.Cargar(a => a.costosEnvio.Any(e => e.idEnvio == id.Value));
                return View(model);
            }
            return HttpNotFound();
        }
        public ActionResult Eliminar(int? id) {
            if (Request.IsAjaxRequest()) {
                if (!id.HasValue)
                    return HttpNotFound();
                var envio = _envios.CargarPorId(id);
                if (envio == null)
                    return HttpNotFound();
                _envios.Eliminar(envio);
                return Json(jsonResult(null, true, envio),JsonRequestBehavior.AllowGet);
            }
            return error404Parcial;
        }
    }
}