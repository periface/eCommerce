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
    public class AreasServicioController : AdminController
    {
        IRepositorioBase<Pais> _pais;
        IRepositorioBase<Estados> _estado;
        IRepositorioBase<Ciudad> _ciudad;
        IRepositorioBase<CostosEnvio> _envios;
        public AreasServicioController(IRepositorioBase<Pais> _pais, IRepositorioBase<Estados> _estado, IRepositorioBase<Ciudad> _ciudad,IRepositorioBase<CostosEnvio>_envios)
        {
            this._pais = _pais;
            this._estado = _estado;
            this._ciudad = _ciudad;
            this._envios = _envios;
        }
        // GET: Administrador/AreasServicio
        public ActionResult Index()
        {
            var paises = _pais.Cargar();
            return View(paises);
        }
        public ActionResult AgregarPais()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarPais(Pais model)
        {
            if (ModelState.IsValid)
            {
                _pais.Agregar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult AgregarEstado(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AgregarEstado(Estados model)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    _estado.Agregar(model);
                }
                return Json(new { data = model });
            }
            return View(model);
        }
        public ActionResult AgregarCiudad(int id)
        {
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AgregarCiudad(Ciudad model)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    _ciudad.Agregar(model);
                }
                return Json(new { data = model });
            }
            return View(model);
        }
        public ActionResult AgregarEnvio(int id) {
            var metodosEnvio = _envios.Cargar();
            ViewBag.id = id;
            return View(metodosEnvio);
        }
        public ActionResult AgregarEnvioEstado(int id)
        {
            var metodosEnvio = _envios.Cargar();
            ViewBag.id = id;
            return View(metodosEnvio);
        }
        public ActionResult AgregarEnvioSeleccionado(int idEnvio,int idEstado) {
            var ciudades = _ciudad.Cargar(a=>a.idEstado==idEstado);
            foreach (var ciudad in ciudades)
            {
                _envios.AgregarRelacion(idEnvio,ciudad.idCiudad);
            }
            return Json(new { mensaje = "Operación completada con exito" },JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarRelacion(int idEnvio, int idCiudad) {
            var envio = _envios.CargarPorId(idEnvio);
            if (envio == null) {
                return Json(new { mensaje = "Envio no encontrado" }, JsonRequestBehavior.AllowGet);
            }
            if (envio.ciudades.Any(a => a.idCiudad == idCiudad)) {
                return Json(new { mensaje = "La ciudad ya cuenta con este envio" }, JsonRequestBehavior.AllowGet);
            }
            var ciudad = _ciudad.CargarPorId(idCiudad);
            if (ciudad == null) {
                return Json(new { mensaje = "Ciudad no encontrada" }, JsonRequestBehavior.AllowGet);
            }
            _envios.AgregarRelacion(idEnvio,idCiudad);
            return Json(new { mensaje = "Envio agregado correctamente" },JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarRelacion(int idEnvio, int idCiudad)
        {
            var envio = _envios.CargarPorId(idEnvio);
            if (envio == null)
            {
                return Json(new { mensaje = "Envio no encontrado" }, JsonRequestBehavior.AllowGet);
            }
            if (!envio.ciudades.Any(a => a.idCiudad == idCiudad))
            {
                return Json(new { mensaje = "La ciudad ya no cuenta con este envio" }, JsonRequestBehavior.AllowGet);
            }
            var ciudad = _ciudad.CargarPorId(idCiudad);
            if (ciudad == null)
            {
                return Json(new { mensaje = "Ciudad no encontrada" }, JsonRequestBehavior.AllowGet);
            }
            _envios.EliminarRelacion(idEnvio, idCiudad);
            return Json(new { mensaje = "Envio removido correctamente" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Estados(int id)
        {
            ViewBag.id = id;
            var estados = _estado.Cargar(a => a.idPais == id);
            return View(estados);
        }
        public ActionResult Ciudades(int id)
        {
            ViewBag.id = id;
            var ciudades = _ciudad.Cargar(a => a.idEstado == id);
            return View(ciudades);
        }
    }
}