using PymeTamFinal.Attributos;
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
        /// <summary>
        /// Esto puede ser hecho en un solo contexto
        /// Pais pais = new Pais();
        /// pais.nombre = "pais";
        /// pais.estados.add(new Estado(){
        ///     nombre = "estado",
        /// });
        /// _paises.Agregar(pais);
        /// </summary>
        IRepositorioBase<Pais> _pais;
        IRepositorioBase<Estados> _estado;
        IRepositorioBase<Ciudad> _ciudad;
        IRepositorioBase<CostosEnvio> _envios;
        public AreasServicioController(IRepositorioBase<Pais> _pais, IRepositorioBase<Estados> _estado, IRepositorioBase<Ciudad> _ciudad, IRepositorioBase<CostosEnvio> _envios)
        {
            this._pais = _pais;
            this._estado = _estado;
            this._ciudad = _ciudad;
            this._envios = _envios;

        }
        // GET: Administrador/AreasServicio
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
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
        public ActionResult EditarPais(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (!id.HasValue)
                    return error404Parcial;
                var pais = _pais.CargarPorId(id);
                if (pais == null)
                    return error404Parcial;
                return View(pais);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditarPais(Pais model)
        {
            if (ModelState.IsValid)
            {
                _pais.Editar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.ErrorBasico, ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        //[AdminAutorizacionParcialAttr(Roles = "Administrador")]
        public ActionResult EliminarPaisVentana(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (!id.HasValue)
                    return error404Parcial;
                var pais = _pais.CargarPorId(id);
                if (pais == null)
                    return error404Parcial;
                return View(pais);
            }
            return HttpNotFound();
        }
        //[Authorize(Roles ="Administrador")]
        public ActionResult EliminarPais(int? id)
        {

            if (!id.HasValue)
                return HttpNotFound();
            var pais = _pais.CargarPorId(id);
            if (pais == null)
                return HttpNotFound();
            _pais.Eliminar(pais);
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Eliminado, ControllerContext.Controller);
            return RedirectToAction("Index");

        }
        public ActionResult EditarEstado(int? id)
        {
            if (Request.IsAjaxRequest()) {
                if (!id.HasValue)
                    return error404Parcial;

                var estado = _estado.CargarPorId(id);
                if (estado == null)
                    return error404Parcial;
                return View(estado);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditarEstado(Estados model) {
            if (Request.IsAjaxRequest()) {
                _estado.Editar(model);
                return Json(new { data = model });
            }
            return HttpNotFound();
        }
        public ActionResult EliminarEstadoVentana(int? id) {
            if (Request.IsAjaxRequest()) {

                if (!id.HasValue)
                    return error404Parcial;
                var estado = _estado.CargarPorId(id);
                if (estado == null)
                    return error404Parcial;
                return View(estado);
            }
            return HttpNotFound();
        }
        public ActionResult EliminarEstado(int? id) {
            if (Request.IsAjaxRequest())
            {

                if (!id.HasValue)
                    return error404Parcial;
                var estado = _estado.CargarPorId(id);
                if (estado == null)
                    return error404Parcial;
                int idPais = estado.idPais;
                _estado.Eliminar(estado);
                var pais = _pais.CargarPorId(idPais);
                return Json(new { data= pais},JsonRequestBehavior.AllowGet);
            }
            return error404Parcial;
        }
        public ActionResult EditarCiudad(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (!id.HasValue)
                    return error404Parcial;
                var ciudad = _ciudad.CargarPorId(id);
                if (ciudad == null)
                    return error404Parcial;
                return View(ciudad);
            }
            return error404Parcial;
        }
        [HttpPost]
        public ActionResult EditarCiudad(Ciudad model) {
            if (Request.IsAjaxRequest()) {
                if (ModelState.IsValid)
                {
                    _ciudad.Editar(model);
                }
                return Json(new { data=model });
            }
            return HttpNotFound();
        }
        public ActionResult EliminarCiudadVentana(int? id)
        {
            if (Request.IsAjaxRequest()) {
                if (!id.HasValue)
                    return error404Parcial;
                var ciudad = _ciudad.CargarPorId(id);
                if (ciudad == null)
                    return error404Parcial;
                return View(ciudad);
            }
            return HttpNotFound();
        }
        public ActionResult EliminarCiudad(int? id) {
            if (Request.IsAjaxRequest()) {
                if (!id.HasValue)
                    return error404Parcial;
                var ciudad = _ciudad.CargarPorId(id);
                if (ciudad == null)
                    return error404Parcial;
                var Estado = _estado.CargarPorId(ciudad.idEstado);
                _ciudad.Eliminar(ciudad);
                return Json(new { data=Estado },JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }
        public ActionResult AgregarEnvio(int id)
        {
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
        public ActionResult AgregarEnvioSeleccionado(int idEnvio, int idEstado)
        {
            var ciudades = _ciudad.Cargar(a => a.idEstado == idEstado);
            foreach (var ciudad in ciudades)
            {
                _envios.AgregarRelacion(idEnvio, ciudad.idCiudad);
            }
            return Json(new { mensaje = "Operación completada con exito" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarRelacion(int idEnvio, int idCiudad)
        {
            var envio = _envios.CargarPorId(idEnvio);
            if (envio == null)
            {
                return Json(new { mensaje = "Envio no encontrado" }, JsonRequestBehavior.AllowGet);
            }
            if (envio.ciudades.Any(a => a.idCiudad == idCiudad))
            {
                return Json(new { mensaje = "La ciudad ya cuenta con este envio" }, JsonRequestBehavior.AllowGet);
            }
            var ciudad = _ciudad.CargarPorId(idCiudad);
            if (ciudad == null)
            {
                return Json(new { mensaje = "Ciudad no encontrada" }, JsonRequestBehavior.AllowGet);
            }
            _envios.AgregarRelacion(idEnvio, idCiudad);
            return Json(new { mensaje = "Envio agregado correctamente" }, JsonRequestBehavior.AllowGet);
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