using PymeTamFinal.Areas.Administrador.PlugIns;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.HtmlHelpers.Abstraccion;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class SeccionesController : Controller
    {
        public const string carpetaSecciones = "/Content/Imagenes/Secciones/";
        IRepositorioBase<Seccion> _secciones;
        public SeccionesController(IRepositorioBase<Seccion>_secciones)
        {
            this._secciones = _secciones;
        }
        // GET: Administrador/Secciones
        public ActionResult Index()
        {
            var secciones = _secciones.Cargar();
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            return View(secciones);
        }
        public ActionResult VerContenido(int id) {
            var seccion = _secciones.CargarPorId(id);
            return View(seccion);
        }
        public ActionResult NuevaSeccion() {
            return View();
        }
        [HttpPost]
        public ActionResult NuevaSeccion(Seccion model,HttpPostedFileBase imagen) {
            if (ModelState.IsValid) {
                _secciones.Agregar(model);
                if (imagen != null) {
                    asignarImagen(model, imagen);

                }
                ServicioDeMensajes.darMensaje(enumMensaje.Agregado,ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EditarSeccion(int? id) {
            if (!id.HasValue)
                return HttpNotFound();
            var model = _secciones.CargarPorId(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditarSeccion(Seccion model,HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid) {
                if (imagen != null) {
                    AdministradorDeArchivos.eliminarArchivo(model.imagenSeccion);
                    model.imagenSeccion = AdministradorDeArchivos.guardarArchivo(imagen, carpetaSecciones, model.idSeccion.ToString());

                }
                _secciones.Editar(model);
                ServicioDeMensajes.darMensaje(enumMensaje.Editado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EliminarVentana(int id) {
            var seccion = _secciones.CargarPorId(id);
            return View(seccion);
        }
        public ActionResult EliminarSeccion(int id)
        {
            var seccion = _secciones.CargarPorId(id);
            _secciones.Eliminar(seccion);
            ServicioDeMensajes.darMensaje(enumMensaje.Eliminado, ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        private void asignarImagen(Seccion model, HttpPostedFileBase imagen)
        {
            model.imagenSeccion = AdministradorDeArchivos.guardarArchivo(imagen,carpetaSecciones,model.idSeccion.ToString());
            _secciones.Editar(model);
        }
    }
}