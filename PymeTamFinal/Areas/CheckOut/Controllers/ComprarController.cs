using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.MetodosPago.CarritoCompra;
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
        public ComprarController(IRepositorioBase<Cliente> _clientes)
        {
            this._clientes = _clientes;
        }
        // GET: CheckOut/Comprar
        [Authorize]
        public ActionResult Resumen(string cupon)
        {
            //No necesito nada solo voy a mostrarle los datos al usuario
            return View();
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