using PymeTamFinal.Areas.Administrador.PlugIns;
using PymeTamFinal.Attributos;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
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
    public class BancosController : AdminController
    {
        private const string carpetaBancos = "/Content/Imagenes/Bancos/";
        IRepositorioBase<Banco> _bancos;
        public BancosController(IRepositorioBase<Banco> _bancos)
        {
            this._bancos = _bancos;
        }
        // GET: Administrador/Bancos
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            var bancos = _bancos.Cargar();
            return View(bancos);
        }
        public ActionResult AgregarBanco()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarBanco(Banco model, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                _bancos.Agregar(model);
                asignarImagen(model, imagen);
                ServicioDeMensajes.darMensaje(enumMensaje.Agregado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EditarBanco(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();
            var banco = _bancos.CargarPorId(id);
            if (banco == null)
                return HttpNotFound();
            return View(banco);
        }
        [HttpPost]
        public ActionResult EditarBanco(Banco model,HttpPostedFileBase imagen) {
            if (ModelState.IsValid) {
                if (imagen != null) {
                    AdministradorDeArchivos.eliminarArchivo(model.bancoImagen);
                    model.bancoImagen = AdministradorDeArchivos.guardarArchivo(imagen,carpetaBancos,model.bancoId.ToString());
                }
                _bancos.Editar(model);
                ServicioDeMensajes.darMensaje(enumMensaje.Editado,ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [AdminAutorizacionParcialAttr]
        public ActionResult EliminarBanco(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();
            var banco = _bancos.CargarPorId(id);
            if (banco == null)
                return HttpNotFound();
            return View(banco);
        }
        public ActionResult Eliminar(int id) {
            var model = _bancos.CargarPorId(id);
            AdministradorDeArchivos.eliminarArchivo(model.bancoImagen);
            _bancos.Eliminar(model);
            ServicioDeMensajes.darMensaje(enumMensaje.Eliminado,ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        private void asignarImagen(Banco model, HttpPostedFileBase imagen)
        {
            string ruta = AdministradorDeArchivos.guardarArchivo(imagen, carpetaBancos, model.bancoId.ToString());
            model.bancoImagen = ruta;
            _bancos.Editar(model);
        }
    }
}