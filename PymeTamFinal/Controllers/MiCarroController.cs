using Microsoft.AspNet.Identity;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.MetodosPago.CarritoCompra;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using PymeTamFinal.Controles;

namespace PymeTamFinal.Controllers
{
    public class MiCarroController : ClienteController
    {
        IRepositorioBase<Cliente> _clientes;
        IRepositorioBase<Precios> _precios;
        public MiCarroController(IRepositorioBase<Cliente> _clientes, IRepositorioBase<Precios> _precios)
        {
            this._clientes = _clientes;
            this._precios = _precios;
        }
        // GET: CarroCompras
        public ViewResult ResumenCarro()
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            CarroResumenViewModel model = new CarroResumenViewModel();
            model.totalItems = carro.cargaItems().Count;
            model.total = carro.cargaTotal();
            return View(model);
        }
        public ActionResult CarritoDetalle()
        {

            var carro = CarroCompras._CarroCompras(HttpContext);
            decimal descuento = 0;
            if (!string.IsNullOrEmpty(cupon))
            {

                var mensaje = carro.AgregarCupon(cupon, HttpContext, out descuento);
                switch (mensaje)
                {
                    case CarroCompras.mensajes.noMinCumplido:
                        ViewBag.estadoCupon = "No cumple con el minimo de compra requerido";
                        break;
                    case CarroCompras.mensajes.cuponCaducado:
                        ViewBag.estadoCupon = "El cupon ha caducado";
                        break;
                    case CarroCompras.mensajes.cuponUsado:
                        ViewBag.estadoCupon = "El cupon ya fue usado";
                        break;
                    case CarroCompras.mensajes.cuponSoloUsuario:
                        ViewBag.estadoCupon = "Cupon no disponible";
                        break;
                    case CarroCompras.mensajes.cuponNoEncontrado:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                    case CarroCompras.mensajes.cuponOk:
                        ViewBag.estadoCupon = "Cupon agregado";
                        ViewBag.cupon = cupon;
                        break;
                    default:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                }
            }
            var model = new CarroDetalleViewModel();
            model.subTotal = carro.cargaTotal();
            model.total = carro.cargaTotal();
            model.items = carro.cargaItems();

            if (descuento < 0)
            {
                ViewBag.descuento = descuento;
            }
            return View(model);
        }
        public ActionResult AgregarAlCarro(int id, int cantidad = 1)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.AgregarAlCarro(id, cantidad);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Comprar(string cupon)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            decimal descuento = 0;
            if (!TieneDatos(User.Identity.GetUserId()))
            {
                return RedirectToAction("MisDatos", "Clientes", new { returnUrl = Url.Action("Comprar", "MiCarro").ToString() });
            }
            if (Session["cupon"] != null)
            {
                if (Session["cupon"].ToString() == cupon)
                {
                    carro.AgregarCupon(cupon, HttpContext, out descuento);
                }
            }
            ViewBag.cupon = cupon;
            var model = new CarroDetalleViewModel();
            model.subTotal = carro.cargaTotal();
            model.total = carro.cargaTotal();
            model.items = carro.cargaItems();
            if (!model.items.Any())
            {
                return RedirectToAction("CarritoDetalle", "MiCarro");
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
        [HttpPost]
        public ActionResult AgregarCupon(string cupon)
        {
            Session["cupon"] = null;
            decimal descuento = 0;
            var carro = CarroCompras._CarroCompras(HttpContext);
            var mensaje = carro.AgregarCupon(cupon, HttpContext, out descuento);
            switch (mensaje)
            {
                case CarroCompras.mensajes.noMinCumplido:
                    ViewBag.estadoCupon = "No cumple con el minimo de compra requerido";
                    break;
                case CarroCompras.mensajes.cuponCaducado:
                    ViewBag.estadoCupon = "El cupon ha caducado";
                    break;
                case CarroCompras.mensajes.cuponUsado:
                    ViewBag.estadoCupon = "El cupon ya fue usado";
                    break;
                case CarroCompras.mensajes.cuponSoloUsuario:
                    ViewBag.estadoCupon = "Cupon no disponible";
                    break;
                case CarroCompras.mensajes.cuponNoEncontrado:
                    ViewBag.estadoCupon = "Cupon no encontrado";
                    break;
                case CarroCompras.mensajes.cuponOk:
                    ViewBag.cupon = cupon;
                    ViewBag.estadoCupon = "Cupon agregado";
                    break;
                default:
                    ViewBag.estadoCupon = "Cupon no encontrado";
                    break;
            }
            ViewBag.descuento = descuento;
            Session["cupon"] = cupon;
            return CarroDetalle;
        }
        public JsonResult Eliminar(int id)
        {
            var model = new CarroEdicionViewModel();
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.EliminaUno(id);
            decimal descuento = 0;
            if (!string.IsNullOrEmpty(cupon))
            {
                var resultado = carro.AgregarCupon(cupon, HttpContext, out descuento);
                switch (resultado)
                {
                    case CarroCompras.mensajes.noMinCumplido:
                        ViewBag.estadoCupon = "No cumple con el minimo de compra requerido";
                        break;
                    case CarroCompras.mensajes.cuponCaducado:
                        ViewBag.estadoCupon = "El cupon ha caducado";
                        break;
                    case CarroCompras.mensajes.cuponUsado:
                        ViewBag.estadoCupon = "El cupon ya fue usado";
                        break;
                    case CarroCompras.mensajes.cuponSoloUsuario:
                        ViewBag.estadoCupon = "Cupon no disponible";
                        break;
                    case CarroCompras.mensajes.cuponNoEncontrado:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                    case CarroCompras.mensajes.cuponOk:
                        ViewBag.estadoCupon = "Cupon agregado";
                        ViewBag.cupon = cupon;
                        break;
                    default:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                }
            }
            var items = carro.cargaRecord(id);
            model.total = items != null ? items.contadorCarro : 0;
            decimal totalOrden = carro.cargaTotal();
            model.subTotal = "$ " + (carro.cargaTotal().ToString() != "0" ? carro.cargaTotal().ToString("#.##") : "0") + " MXN";
            if (descuento < 0)
            {
                totalOrden += descuento;

                model.descuento = "Descuento <span> $ " + descuento.ToString("#.##") + " MXN </span>";
            }
            else
            {
                model.descuento = "Descuento <span> Sin descuento </span>";
            }
            model.totalRecord = "$ " + calcularPrecio(items) + " MXN";
            model.totalCompleto = "$ " + totalOrden + " MXN";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private string calcularPrecio(CarritoDeCompra items)
        {
            if (items != null)
                return (precioProducto(items.producto) * items.contadorCarro).ToString();
            return "0";
        }

        public JsonResult Agregar(int id)
        {
            var model = new CarroEdicionViewModel();
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.AgregaUno(id);
            decimal descuento = 0;
            if (!string.IsNullOrEmpty(cupon))
            {
                var resultado = carro.AgregarCupon(cupon, HttpContext, out descuento);
                switch (resultado)
                {
                    case CarroCompras.mensajes.noMinCumplido:
                        ViewBag.estadoCupon = "No cumple con el minimo de compra requerido";
                        break;
                    case CarroCompras.mensajes.cuponCaducado:
                        ViewBag.estadoCupon = "El cupon ha caducado";
                        break;
                    case CarroCompras.mensajes.cuponUsado:
                        ViewBag.estadoCupon = "El cupon ya fue usado";
                        break;
                    case CarroCompras.mensajes.cuponSoloUsuario:
                        ViewBag.estadoCupon = "Cupon no disponible";
                        break;
                    case CarroCompras.mensajes.cuponNoEncontrado:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                    case CarroCompras.mensajes.cuponOk:
                        ViewBag.estadoCupon = "Cupon agregado";
                        ViewBag.cupon = cupon;
                        break;
                    default:
                        ViewBag.estadoCupon = "Cupon no encontrado";
                        break;
                }
            }
            var items = carro.cargaRecord(id);
            model.total = items.contadorCarro;
            decimal totalOrden = carro.cargaTotal();
            model.subTotal = "$ " + carro.cargaTotal().ToString("#.##") + " MXN";
            if (descuento < 0)
            {
                totalOrden += descuento;

                model.descuento = "Descuento <span> $ " + descuento.ToString("#.##") + " MXN </span>";
            }
            else
            {
                model.descuento = "Descuento <span> Sin descuento </span>";
            }
            model.totalRecord = "$ " + (precioProducto(items.producto) * items.contadorCarro).ToString() + " MXN";
            model.totalCompleto = "$ " + totalOrden + " MXN";
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private decimal precioProducto(Producto producto)
        {
            var precio = _precios.CargarPorId(producto.idProducto);
            if (precio.descuentoActivo)
            {
                return precio.precioEsp;
            }
            return precio.precio;
        }

        //FUNCIONA COMO ELIMINAR UNO
        public ActionResult EliminarDelCarro(int id)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.RemoverDelCarro(id);
            return CarroDetalle;
        }
        public ActionResult AgregarUno(int id)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.AgregaUno(id);
            return CarroDetalle;
        }
        public ActionResult LimpiarCarro()
        {
            var carro = CarroCompras._CarroCompras(HttpContext);

            return CarroDetalle;
        }
        private void actualizarFechaDelCarro()
        {
            //HangFire
            //-Actualiza la fecha de expiración cada vez que se agregue un nuevo producto

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
        private ActionResult CarroDetalle
        {
            get
            {
                return RedirectToAction("CarritoDetalle", "MiCarro");
            }
        }
    }
}