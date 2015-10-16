using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.MetodosPago.CarritoCompra;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using Rotativa;
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
        IRepositorioBase<Empresa> _empresa;
        ITransaccionExterna<paypalPagoClienteModel> _paypal;
        IRepositorioBase<Banco> _bancos;
        public ComprarController(IRepositorioBase<Cliente> _clientes,
            IOrdenGeneradorBase<compraModel> _orden,
            IRepositorioBase<CostosEnvio> _envios,
            IRepositorioBase<Pais> _pais,
            IRepositorioBase<Estados> _estado,
            IRepositorioBase<Empresa> _empresa,
            ITransaccionExterna<paypalPagoClienteModel> _paypal, IRepositorioBase<PaypalConfig> _configPaypal, IPaypalCryptBase<PaypalConfig> _paypalEncrypService,IRepositorioBase<Banco>_bancos) : base(_configPaypal, _paypalEncrypService)
        {
            this._clientes = _clientes;
            this._orden = _orden;
            this._envios = _envios;
            this._pais = _pais;
            this._estado = _estado;
            this._paypal = _paypal;
            this._empresa = _empresa;
            this._bancos = _bancos;
        }
        public ActionResult MetodosDisponibles()
        {
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
            resolverCupon(cupon, carro);
            return View();
        }
        public ActionResult EnvioCosto(int id)
        {
            var envio = _envios.CargarPorId(id);
            decimal total = calculaTotal(envio.costo);
            return Json(new { total = "$ " + total.ToString("#.##") + " MXN", costoEnvio = "$ " + envio.costo.ToString("#.##") + " MXN" }, JsonRequestBehavior.AllowGet);
        }

        private decimal calculaTotal(decimal costo)
        {
            decimal descuento = 0;
            decimal subTotal = 0;
            var carro = CarroCompras._CarroCompras(HttpContext);
            var total = carro.cargaTotal();
            if (_cupon != null)
            {
                carro.AgregarCupon(_cupon, HttpContext, out descuento);
            }
            if (descuento < 0)
            {
                subTotal = total + descuento;
            }
            else
            {
                subTotal = total;
            }
            return subTotal + costo;
        }

        private void resolverCupon(string cupon, CarroCompras carro)
        {
            decimal descuento = 0;
            if (!string.IsNullOrEmpty(_cupon) && cupon == _cupon)
            {
                carro.AgregarCupon(_cupon, HttpContext, out descuento);
                ViewBag.descuento = descuento;
            }
            else
            {
                Session["cupon"] = null;
                return;
            }
        }

        public ActionResult Condiciones(int id)
        {
            if (id == 0)
            {
                return View(new CostosEnvio()
                {
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
        [HandleError(View = "ErrorPayPal")]
        public ActionResult Comprar(compraModel model)
        {
            if (model.idEnvio == 0)
            {
                return RedirectToAction("Resumen", "Comprar", new { cupon = _cupon });
            }
            decimal descuento = 0;
            model.cupon = _cupon;
            model.email = User.Identity.Name;
            var carro = CarroCompras._CarroCompras(HttpContext);
            if (!carro.cargaItems().Any())
                return DetalleCarro;
            carro.AgregarCupon(_cupon, HttpContext, out descuento);
            model.total = carro.cargaTotal();
            _orden.guardarOrden(model, carro.cargaId(HttpContext), userId, descuento, HttpContext);
            //El id de orden se ha almacenado de forma temporal 
            //Vaciar carro != cancelarCom

            carro.vaciarCarro();

            limpiarCupon();
            switch (model.pago)
            {
                case "Paypal":
                    return paypalTransaccion;
                case "Deposito":
                    return depositoInfo;
                case "Credito":
                    return credito;
                default:
                    break;
            }
            return View(model);
        }
        public ActionResult PayPal()
        {
            var contextId = _orden.cargaContexto(HttpContext);
            var pagoClienteModel = new paypalPagoClienteModel()
            {
                pedido = contextId,
                tipoMoneda = "MXN",
                cancelUrl = Url.Action("Cancelar", "Comprar", new { Area = "CheckOut" }, protocol: Request.Url.Scheme).ToString() + "&cancel=true",
                returnUrl = Url.Action("Confirmar", "Comprar", new { Area = "CheckOut" }, protocol: Request.Url.Scheme).ToString()
            };
            var paypalServiceModel = activePayPalApi;
            var tokenPayPal = _paypal.GenerarToken(pagoClienteModel, paypalServiceModel.decryptedId, paypalServiceModel.decryptedSecret);
            ViewBag.paypalToken = tokenPayPal;
            return View();
        }
        public ActionResult Confirmar(string paymentId, string PayerID)
        {
            var id = _orden.cargaContexto(HttpContext);
            int idOrden;
            ViewBag.paymentId = paymentId;
            ViewBag.PayerID = PayerID;
            bool ok = int.TryParse(id.ToString(), out idOrden);
            if (ok)
            {
                return View(_orden.cargaOrden(id));
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Confirmar(FormCollection forms, string paymentId, string PayerID)
        {
            var paypalServiceModel = activePayPalApi;
            var id = _orden.cargaContexto(HttpContext);
            int idOrden;
            bool ok = int.TryParse(id.ToString(), out idOrden);
            if (ok)
            {
                if (_paypal.EjecutarPago(paymentId, idOrden, PayerID, paypalServiceModel.decryptedId, paypalServiceModel.decryptedSecret))
                {
                    return RedirectToAction("Finalizado", new { id = idOrden });
                }
                else
                {
                    return View("ErrorPayPal");
                }
            }
            else
            {
                //La sesión esta terminada pero el usuario puede pagar la orden despues
                return View("SesionTerminada");
            }
        }
        public ActionResult Finalizado(int id)
        {
            var _id = _orden.cargaContexto(HttpContext);
            if (_id <= 0 || id != _id)
            {
                return View("SesionTerminada");
            }
            ViewBag.id = id;
            _orden.limpiaContexto(HttpContext);
            return View();
        }
        public ActionResult Recibo(int id)
        {
            //Falta Validar que el recibo sea del usuario
            ReciboViewModel model = new ReciboViewModel();
            var orden = _orden.cargaOrden(id);
            if (orden == null)
            {
                return HttpNotFound();
            }
            var cliente = _clientes.Cargar(a => a.idAsp == userId).SingleOrDefault();
            if (cliente == null)
            {
                return HttpNotFound();
            }
            model.orden = (Orden)orden;
            if (!model.orden.ordenPagado) {
                return HttpNotFound();
            }
            if (model.orden.idCliente != cliente.idCliente)
            {
                return HttpNotFound();
            }
            model.cliente = _clientes.CargarPorId(model.orden.idCliente);
            var empresa = _empresa.Cargar(a => a.infoActiva == true).SingleOrDefault();
            model.direccionEmpresa = empresa.direccionEmpresa;
            model.nombreEmpresa = empresa.nombreEmpresa;
            model.emailEmpresa = empresa.correoVentasEmpresa;
            model.fecha = DateTime.Now.ToShortDateString();
            model.idOrden = model.orden.idOrden;
            model.logoEmpresa = empresa.imgPrincipalEmpresa;
            model.telefonoEmpresa = empresa.telefonoEmpresa;
            model.razonSocialEmpresa = empresa.razonSocial;
            return new ViewAsPdf(model);
        }
        public ActionResult Credito()
        {
            return View();
        }
        public ActionResult Deposito()
        {
            var contexto = _orden.cargaContexto(HttpContext);
            if (contexto <=0) {
                return View("SesionTerminada");
            }
            ViewBag.id = contexto;
            var bancos = _bancos.Cargar(a => a.bancoActivo == true);
            _orden.limpiaContexto(HttpContext);
            return View(bancos);
        }
        [HttpPost]
        public ActionResult Check(string token)
        {
            return Redirect(string.Format(paypalEndPoint, token));
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
        private string _cupon
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
        private void limpiarCupon() {
            Session["cupon"] = null;
        }
    }
}