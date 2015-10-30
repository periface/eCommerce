﻿using Microsoft.AspNet.Identity;
using PymeTamFinal.Areas.CheckOut.Models;
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

    [Authorize]
    public class ComprarController : CheckOutController
    {
        private const string rutaTickets = "/Content/Imagenes/Tickets/";
        IRepositorioBase<Cliente> _clientes;
        IOrdenGeneradorBase<CompraModel> _orden;
        IRepositorioBase<CostosEnvio> _envios;
        IRepositorioBase<Pais> _pais;
        IRepositorioBase<Estados> _estado;
        IRepositorioBase<Empresa> _empresa;
        ITransaccionExterna<PaypalPagoClienteModel> _paypal;
        IRepositorioBase<Banco> _bancos;
        ITransaccionExterna<StripeTarjetaModel> _tarjeta;
        IRepositorioBase<Orden> _ordenDb;
        public ComprarController(IRepositorioBase<Cliente> _clientes,
            IOrdenGeneradorBase<CompraModel> _orden,
            IRepositorioBase<CostosEnvio> _envios,
            IRepositorioBase<Pais> _pais,
            IRepositorioBase<Estados> _estado,
            IRepositorioBase<Empresa> _empresa,
            ITransaccionExterna<PaypalPagoClienteModel> _paypal, IRepositorioBase<PaypalConfig> _configPaypal, IPaypalCryptBase<PaypalConfig> _paypalEncrypService, IRepositorioBase<Banco> _bancos, ITransaccionExterna<StripeTarjetaModel> _tarjeta, IRepositorioBase<Orden> _ordenDb) : base(_configPaypal, _paypalEncrypService)
        {
            this._clientes = _clientes;
            this._orden = _orden;
            this._envios = _envios;
            this._pais = _pais;
            this._estado = _estado;
            this._paypal = _paypal;
            this._empresa = _empresa;
            this._bancos = _bancos;
            this._tarjeta = _tarjeta;
            this._ordenDb = _ordenDb;
        }
        public ActionResult MetodosDisponibles()
        {
            return View();
        }
        public ActionResult PagoTardio(int? id)
        {
            if (id.HasValue)
            {
                var pedido = _orden.cargaOrden(id);
                return View((Orden)pedido);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult PagoTardio(string pago, int? id)
        {
            _orden.actualizarMetodoDePago(id, pago, HttpContext);
            switch (pago)
            {
                case "Paypal":
                    var paypal = new metodoPagoPayPal(Url.Action("PayPal", "Comprar", new { Area = "CheckOut" }).ToString());
                    return paypal.result;
                case "Deposito":
                    return new metodoPagoDeposito(Url.Action("Deposito", "Comprar", new { Area = "CheckOut", metodo = pago }).ToString()).result;
                case "Credito":
                    return new metodoPagoTarjeta(Url.Action("PayPal", "Comprar", new { Area = "CheckOut" }).ToString()).result;
                default:
                    break;
            }
            return View();
        }
        // GET: CheckOut/Comprar
        public ActionResult Resumen(string cupon)
        {
            if (TieneDatos(userId)) {


                var carro = CarroCompras._CarroCompras(HttpContext);
                if (!carro.cargaItems().Any())
                    return DetalleCarro;
                //No necesito nada solo voy a mostrarle los datos al usuario
                resolverCupon(cupon, carro);
                return View();
            }
            return RedirectToAction("MisDatos","Clientes", new { Area="",returnUrl = Url.Action("Resumen","Comprar","CheckOut") });
        }
        public ActionResult EnvioCosto(int id)
        {
            var envio = _envios.CargarPorId(id);
            decimal total = calculaTotal(envio.costo);
            return Json(new { total = "$ " + total.ToString("#.##") + " MXN", costoEnvio = "$ " + envio.costo.ToString("#.##") + " MXN" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Comprar(CompraModel model)
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
                    var paypal = new metodoPagoPayPal(Url.Action("PayPal", "Comprar", new { Area = "CheckOut" }).ToString());
                    return paypal.result;
                case "Deposito":
                    return new metodoPagoDeposito(Url.Action("Deposito", "Comprar", new { Area = "CheckOut" }).ToString()).result;
                case "Credito":
                    return new metodoPagoTarjeta(Url.Action("Credito", "Comprar", new { Area = "CheckOut" }).ToString()).result;
                default:
                    break;
            }
            return View(model);
        }
        public ActionResult PayPal()
        {
            var contextId = _orden.cargaContexto(HttpContext);
            var pagoClienteModel = new PaypalPagoClienteModel()
            {
                pedido = contextId,
                tipoMoneda = "MXN",
                cancelUrl = Url.Action("Cancelar", "Comprar", new { Area = "CheckOut" }, protocol: Request.Url.Scheme).ToString() + "?cancel=true",
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
                return SesionTerminada;
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
            if (!model.orden.ordenPagado)
            {
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
            var contexto = _orden.cargaContexto(HttpContext);
            if (contexto == 0)
                return SesionTerminada;
            return View();
        }
        [HttpPost]
        public ActionResult Credito(PayPalTarjetaModel model)
        {
            if (ModelState.IsValid)
            {
                //var TOKEN = _tarjeta.GenerarToken(model, "sk_test_k3OHmjXrTHD5Ekdt9ztz4D7Z", "");
                //_tarjeta.EjecutarPago(TOKEN, _orden.cargaContexto(HttpContext), "", "sk_test_k3OHmjXrTHD5Ekdt9ztz4D7Z", "");
            }
            return View();
        }
        public ActionResult Deposito(string metodo)
        {

            var contexto = _orden.cargaContexto(HttpContext);
            if (contexto <= 0)
            {
                return SesionTerminada;
            }
            ViewBag.id = contexto;
            var bancos = _bancos.Cargar(a => a.bancoActivo == true);
            _orden.limpiaContexto(HttpContext);
            return View(bancos);
        }
        [HttpPost]
        public ActionResult SubirTicket(int? id)
        {
            string ruta = string.Empty;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var imagen = Request.Files[i];
                    ruta = Administrador.PlugIns.AdministradorDeArchivos.guardarArchivo(imagen, rutaTickets, id.Value.ToString());
                    _orden.agregarTicket(id, ruta);
                }
                return Json(new { ok = true, ruta = ruta });
            }
            else
            {
                return Json(new { ok = false });
            }
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
        #region Helpers
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
        private void limpiarCupon()
        {
            Session["cupon"] = null;
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
        #endregion
    }
}