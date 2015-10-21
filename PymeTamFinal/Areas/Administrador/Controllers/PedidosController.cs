using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PedidosController : AdminController
    {
        IRepositorioBase<Orden> _orden;
        public PedidosController(IRepositorioBase<Orden> _orden)
        {
            this._orden = _orden;
        }
        // GET: Administrador/Pedidos
        public ActionResult Index()
        {
            return View(_orden.Cargar());
        }
        public ActionResult AgregarCodigoRastreo(int? id) {
            return View();
        }
        public ActionResult MarcarComoPagado(int? id) {
            return View();
        }
        public ActionResult CancelarPedido(int? id) {
            return View();
        }
        public ActionResult EditarEstado(int? id) {
            return View();
        }
        public ActionResult VerDetalles(int? id) {
            return View();
        }
        public ActionResult InvalidarTicket(int? id) {
            return View();
        }
        public ActionResult ValidarTicket(int? id) {
            return View();
        }
    }
}