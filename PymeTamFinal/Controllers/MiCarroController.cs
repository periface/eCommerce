using PymeTamFinal.MetodosPago.CarritoCompra;
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
        public ActionResult EstadoCarrito()
        {
            return View();
        }
        public ActionResult CarritoDetalle() {
            var carro = CarroCompras._CarroCompras(HttpContext);
            return View();
        }
        public ActionResult AgregarAlCarro(int id, int cantidad = 1)
        {
            var carro = CarroCompras._CarroCompras(HttpContext);
            carro.AgregarAlCarro(id,cantidad);
            return CarroDetalle;
        }
        public ActionResult AgregarAlCarroAjax(int id)
        {
            return View();
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