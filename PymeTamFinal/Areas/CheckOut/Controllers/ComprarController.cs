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
        IOrdenGeneradorBase<compraModel> _orden;
        IRepositorioBase<CostosEnvio> _envios;
        IRepositorioBase<Pais> _pais;
        IRepositorioBase<Estados> _estado;
        ITransaccionExterna<paypalPagoClienteModel> _paypal;
        public ComprarController(IRepositorioBase<Cliente> _clientes,
            IOrdenGeneradorBase<compraModel>_orden,
            IRepositorioBase<CostosEnvio>_envios,
            IRepositorioBase<Pais>_pais,
            IRepositorioBase<Estados>_estado,
            ITransaccionExterna<paypalPagoClienteModel> _paypal)
        {
            this._clientes = _clientes;
            this._orden = _orden;
            this._envios = _envios;
            this._pais = _pais;
            this._estado = _estado;
            this._paypal = _paypal;
        }
        public ActionResult MetodosDisponibles() {
            return View();
        }
        // GET: CheckOut/Comprar
        [Authorize]
        public ActionResult Resumen(string cupon)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            if (!carro.cargaItems().Any())
                return DetalleCarro;
            //No necesito nada solo voy a mostrarle los datos al usuario

            return View();
        }
        public ActionResult Condiciones(int id) {
            if (id == 0) {
                return View(new CostosEnvio() {
                    detalle = "Seleccione un metodo de envió",
                });
            }
            var envio = _envios.CargarPorId(id);
            return View(envio);
        }
        public ViewResult EnviosDisp()
        {
            var cliente = _clientes.Cargar(a => a.idAsp == userId).SingleOrDefault();

            return View(envios(cliente.idEstado));
        }
        public ActionResult CancelarCompra()
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.cancelarCarro();
            return DetalleCarro;
        }
        [HttpPost]
        public ActionResult Comprar(compraModel model)
        {
            if (model.idEnvio == 0) {
                return RedirectToAction("Resumen","Comprar", new { cupon = cupon });
            }
            decimal descuento = 0;
            model.cupon = cupon;
            model.email = User.Identity.Name;
            var carro = CarroCompras._CarroCompras(HttpContext);
            if (!carro.cargaItems().Any())
                return DetalleCarro;
            carro.AgregarCupon(cupon, HttpContext, out descuento);
            model.total = carro.cargaTotal();
            _orden.guardarOrden(model, carro.cargaId(HttpContext), userId, descuento, HttpContext);
            //El id de orden se ha almacenado de forma temporal 
            //Vaciar carro != cancelarCom
            carro.vaciarCarro();
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
        public ActionResult PayPal()
        {
            var contextId = _orden.cargaContexto(HttpContext);
            var pagoClienteModel = new paypalPagoClienteModel() {
                pedido = contextId,
                tipoMoneda = "MXN",
                cancelUrl = Url.Action("Cancelar", "Comprar", new { Area = "CheckOut" }, protocol: Request.Url.Scheme).ToString(),
                returnUrl = Url.Action("Confirmar", "Comprar", new { Area = "CheckOut" }, protocol: Request.Url.Scheme).ToString()
            };
            var tokenPayPal = _paypal.GenerarToken(pagoClienteModel);
            ViewBag.paypalToken = tokenPayPal;
            return View();
        }
        [HttpPost]
        public ActionResult Check(string token) {
            return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + token + "");
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
        private List<CostosEnvio> envios(int idEstado)
        {
            return _envios.Cargar(a => a.estados.Any(e => e.idEstado == idEstado)).ToList();
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