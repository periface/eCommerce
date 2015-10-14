using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.MetodosPago.CarritoCompra;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Controllers
{
    public class ComprarController : CheckOutController
    {
        IRepositorioBase<Cliente> _clientes;
        ITransaccionBase<compraModel> _orden;
        public ComprarController(IRepositorioBase<Cliente> _clientes, ITransaccionBase<compraModel> _orden)
        {
            this._clientes = _clientes;
            this._orden = _orden;
        }
        // GET: CheckOut/Comprar
        [Authorize]
        public ActionResult Resumen(string cupon)
        {
            //No necesito nada solo voy a mostrarle los datos al usuario
            return View();
        }
        [HttpPost]
        public ActionResult Comprar(compraModel model)
        {
            model.cupon = cupon;
            var carro = CarroCompras._CarroCompras(HttpContext);
            _orden.guardarOrden(model, carro.cargaId(HttpContext), userId);
            switch (model.pago)
            {
                case "Paypal":
                    return paypalTransaccion;
                case "Deposito":
                    return depositoInfo;
                case "Otro":
                    return otroInfo;
                default:
                    break;
            }
            return View(model);
        }
        public bool TieneDatos(string idusuario)
        {
            var usuario = _clientes.Cargar(a => a.idAsp == idusuario).SingleOrDefault();
            if (usuario != null)
            {
                if (usuario.datosCapturados)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private string cupon
        {
            get
            {
                if (Session["cupon"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Session["cupon"].ToString();
                }
            }
        }
        private string userId
        {
            get
            {
                return User.Identity.GetUserId();
            }
        }
    }
}