using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        IRepositorioBase<Cliente> _clientes;
        public ClientesController(IRepositorioBase<Cliente> _clientes)
        {
            this._clientes = _clientes;
        }
        // GET: Clientes
        public ActionResult MisDatos(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            var cliente = _clientes.Cargar(a=>a.idAsp==userId).SingleOrDefault();
            if (cliente == null) {
                var _cliente = new Cliente();
                return View(_cliente);
            }
            return View(cliente);
        }
        [HttpPost]
        public ActionResult CapturaCliente(Cliente cliente,string returnUrl) {
            if (cliente.idCliente != 0)
            {
                //Edicion
                cliente.datosCapturados = capturoDatos(cliente);
                cliente.facturacionCapturada = capturoDatosFac(cliente);
                _clientes.Editar(cliente);
            }
            else {
                //Nuevo
                cliente.datosCapturados = capturoDatos(cliente);
                cliente.facturacionCapturada = capturoDatosFac(cliente);
                _clientes.Agregar(cliente);
            }
            if (!string.IsNullOrEmpty(returnUrl)) {
                return RedirectToRoute(returnUrl);
            }
            return RedirectToAction("MisDatos");
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