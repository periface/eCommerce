using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaController : Controller
    {
        IRepositorioBase<Producto> _producto;
        public TiendaController(IRepositorioBase<Producto>_producto)
        {
            this._producto = _producto;
        }
        // Inicio de la tienda
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetalleProducto(int id,string slug) {
            return View();
        }
        public ActionResult Productos(int? idCategoria, int? pagina, string orden, string busquedaString, string busqueda, int? min, int? max) {
            var productos = _producto.Cargar(a => a.stock > 0 || a.mostrarSinStock == true && a.habilitado == true).OrderByDescending(a => a.fechaCreacion);

            return View(productos);
        }
        public ActionResult Ayuda() {
            return View();
        }
    }
}