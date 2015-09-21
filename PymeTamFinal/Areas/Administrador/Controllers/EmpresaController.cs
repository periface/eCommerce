using PymeTamFinal.Areas.Administrador.PlugIns;
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
    public class EmpresaController : Controller
    {
        public const string carpetaEmpresa = "/Content/Imagenes/Empresa/";
        IRepositorioBase<Empresa> _empresa { get; set; }
        public EmpresaController(IRepositorioBase<Empresa>_empresa)
        {
            this._empresa = _empresa;
        }
        // GET: Administrador/Empresa
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            var model = _empresa.Cargar();
            return View(model);
        }
        public ActionResult NuevaInformacion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NuevaInformacion(Empresa model,HttpPostedFileBase imagen) {
            if (ModelState.IsValid) {
                _empresa.Agregar(model);
                if (imagen != null) {
                    asignarImagen(model,imagen);
                }
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado,ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private void asignarImagen(Empresa model, HttpPostedFileBase imagen)
        {
            var url = AdministradorDeArchivos.guardarArchivo(imagen,carpetaEmpresa,model.idEmpresa.ToString());
            model.imgPrincipalEmpresa = url;
            _empresa.Editar(model);
        }
        public ActionResult ActivarInfo(int id) {
            var info = _empresa.CargarPorId(id);
            info.infoActiva = true;
            _empresa.Editar(info);
            desactivarOtros(info.idEmpresa);
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Habilitado, ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        private void desactivarOtros(int idEmpresa)
        {
            var otros = _empresa.Cargar(a=>a.idEmpresa!=idEmpresa).ToList();
            foreach (var empresa in otros)
            {
                empresa.infoActiva = false;
                _empresa.Editar(empresa);
            }
        }

        public ActionResult EditarInformacion(int? id) {
            if (!id.HasValue)
                return HttpNotFound();
            var model = _empresa.CargarPorId(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditarInformacion(Empresa model,HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid) {
                if (imagen != null) {
                    AdministradorDeArchivos.eliminarArchivo(model.imgPrincipalEmpresa);
                    model.imgPrincipalEmpresa = AdministradorDeArchivos.guardarArchivo(imagen,carpetaEmpresa,model.idEmpresa.ToString());
                }
                _empresa.Editar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult EliminarInformacion(int id) {
            var empresa = _empresa.CargarPorId(id);
            if (empresa.infoActiva) {
                activaOtro(id);
            }
            _empresa.Eliminar(empresa);
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Eliminado, ControllerContext.Controller);
            return RedirectToAction("Index");
        }

        private void activaOtro(int id)
        {
            var otro = _empresa.Cargar(a => a.idEmpresa != id);
            if (!otro.Any()) {
                return;
            }
            var _otr = otro.First();
            _otr.infoActiva = true;
            _empresa.Editar(_otr);
        }
    }
}