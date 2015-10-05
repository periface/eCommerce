using PymeTamFinal.MetodosPago.CarritoCompra;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class MiCarroController : Controller
    {
        // GET: CarroCompras
        public ViewResult ResumenCarro() {
            return View();
        }
        public ActionResult CarritoDetalle() {
            
            var carro = CarroCompras._CarroCompras(HttpContext);
            decimal descuento = 0;
            if (Session["cupon"] != null)
            {
                var cupon = Session["cupon"].ToString();
                var mensaje = carro.AgregarCupon(cupon,HttpContext,out descuento);
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
            if (descuento < 0) {
                ViewBag.descuento = descuento;
            }
            return View(model);
        }
        public ActionResult AgregarAlCarro(int id, int cantidad = 1)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.AgregarAlCarro(id,cantidad);
            return Json(new { ok=true },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Comprar(string cupon) {
            if (Session["cupon"] != null) {
                if (Session["cupon"].ToString() == cupon) {

                }
            }
            var carro = CarroCompras._CarroCompras(HttpContext);
            var model = new CarroDetalleViewModel();
            model.subTotal = carro.cargaTotal();
            model.total = carro.cargaTotal();
            model.items = carro.cargaItems();
            return View(model);
        }
        [HttpPost]
        public ActionResult AgregarCupon(string cupon) {
            Session["cupon"] = null;
            decimal descuento = 0;
            var carro = CarroCompras._CarroCompras(HttpContext);
            var mensaje = carro.AgregarCupon(cupon,HttpContext,out descuento);
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
        public ActionResult LimpiarCarro() {
            var carro = CarroCompras._CarroCompras(HttpContext);
            
            return CarroDetalle;
        }
        private void actualizarFechaDelCarro() {
            //HangFire
                //-Actualiza la fecha de expiración cada vez que se agregue un nuevo producto
                
        }
        private ActionResult CarroDetalle {
            get {
                return RedirectToAction("CarritoDetalle", "MiCarro");
            }
        }
    }
}