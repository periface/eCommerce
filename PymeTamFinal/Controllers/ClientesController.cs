using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    [Authorize]
    public class ClientesController : ClienteController
    {
        IRepositorioBase<Cliente> _clientes;
        IRepositorioBase<Pais> _paises;
        IRepositorioBase<Estados> _estados;
        public ClientesController(IRepositorioBase<Cliente> _clientes, IRepositorioBase<Pais> _paises, IRepositorioBase<Estados> _estados)
        {
            this._clientes = _clientes;
            this._paises=_paises;
            this._estados = _estados;
        }
        // GET: Clientes
        public ActionResult MisDatos(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.paises = paises;
            ViewBag.estados = estados;
            var cliente = _clientes.Cargar(a=>a.idAsp==userId).SingleOrDefault();
            if (cliente == null) {
                var _cliente = new Cliente();
                return View(_cliente);
            }
            if (ControllerContext.IsChildAction) {
                return View("_datosUsuario",cliente);
            }
            return View(cliente);
        }
        public JsonResult cargaEstados(int id) {
            return cargaEstadosPorId(id);
        }
        public JsonResult nombreDisponible(Cliente cliente) {
            var usuario = _clientes.Cargar(a=>a.nombreUsuario==cliente.nombreUsuario && a.idAsp!=userId).SingleOrDefault();
            if (usuario!=null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(true,JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult CapturaCliente(Cliente cliente,string returnUrl) {
            if (ModelState.IsValid) {
                if (cliente.idCliente != 0)
                {
                    //Edicion
                    cliente.idAsp = userId;
                    cliente.datosCapturados = capturoDatos(cliente);
                    cliente.facturacionCapturada = capturoDatosFac(cliente);
                    _clientes.Editar(cliente);
                }
                else
                {
                    //Nuevo
                    cliente.datosCapturados = capturoDatos(cliente);
                    cliente.idAsp = userId;
                    cliente.facturacionCapturada = capturoDatosFac(cliente);
                    _clientes.Agregar(cliente);
                }
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("MisDatos");
            }
            return View(cliente);
            
        }

        private bool capturoDatos(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.direccionEnvioLinea1)
                || string.IsNullOrEmpty(cliente.nombre)
                || string.IsNullOrEmpty(cliente.apPaterno)
                || string.IsNullOrEmpty(cliente.cp) || string.IsNullOrEmpty(cliente.ciudad) || string.IsNullOrEmpty(cliente.telefono))
            {
                return false;
            }
            else {
                return true;
            }
        }
        private bool capturoDatosFac(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.razonSocial)
                || string.IsNullOrEmpty(cliente.direccionFacturacionLinea1)
                || string.IsNullOrEmpty(cliente.direccionFacturacionLinea2)
                || string.IsNullOrEmpty(cliente.rfc))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public ActionResult MisPedidos() {
            return View();
        }
        public ActionResult Seguridad() {
            return View();
        }
        private string userId {
            get {
                return User.Identity.GetUserId();
            }
        }
    }
}