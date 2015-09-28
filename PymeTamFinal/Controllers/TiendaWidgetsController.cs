using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaWidgetsController : Controller
    {
        IRepositorioBase<Empresa> _empresa;
        IRepositorioBase<Producto> _productos;
        IRepositorioBase<Categoria> _categorias;
        IRepositorioBase<Precios> _precios;
        IRepositorioBase<CajaComentarios> _comentarios;
        public TiendaWidgetsController(IRepositorioBase<Empresa> _empresa, IRepositorioBase<Producto> _productos, IRepositorioBase<Categoria> _categorias, IRepositorioBase<Precios> _precios, IRepositorioBase<CajaComentarios> _comentarios)
        {
            this._empresa = _empresa;
            this._productos = _productos;
            this._categorias = _categorias;
            this._precios = _precios;
            this._comentarios = _comentarios;
        }
        // GET: TiendaWidgets
        //Solo 2 temas por ahora. Eshopper y Bootstrap basico (Este ultimo podra ser modificable)
        public ActionResult MenuTienda()
        {
            List<CategoriasMenuRapido> menu = new List<CategoriasMenuRapido>();
            foreach (var item in _categorias.Cargar(a => a.idPadre == 0))
            {
                menu.Add(new CategoriasMenuRapido
                {
                    idCategoria = item.idCategoria,
                    nombreCategoria = item.nombreCategoria
                });
            }
            return View("_menuTiendaEshopper", menu);
        }
        public ActionResult NovedadesSlider()
        {
            return View("_SliderNovedades");
        }
        public ActionResult ProductosNuevos()
        {
            var modelo = cargaProductosNuevos();
            return View("_productosNuevosSlider", modelo);
        }
        public ActionResult ProductosRecomendados()
        {
            var modelo = cargaProductosNuevos();
            return View("_productosRecomendadosSlider", modelo);
        }

        private List<ProductoListaViewModel> cargaProductosNuevos()
        {
            List<ProductoListaViewModel> lista = new List<ProductoListaViewModel>();
            foreach (var item in _productos.Cargar(a => a.stock > 0 || a.mostrarSinStock == true && a.habilitado == true).OrderByDescending(a => a.fechaCreacion))
            {
                var precio = _precios.CargarPorId(item.idProducto);
                //Ignora productos sin precio definido
                if (precio != null)
                {
                    lista.Add(new ProductoListaViewModel()
                    {
                        descCorta = item.descripcionCorta,
                        idProducto = item.idProducto,
                        imagen = item.imgProducto,
                        slug = item.slugs,
                        nombreProducto = item.nombreProducto,
                        precio = cargaPrecio(precio),
                        calificacionProm = cargaProm(item.idProducto)
                    });
                }

            }
            return lista;
        }

        private int cargaProm(int idProducto)
        {
            var comentarios = _comentarios.Cargar(a => a.idProducto == idProducto && a.habilitado == true);
            double promedio = 0;
            if (comentarios.Any())
            {
                promedio = comentarios.Average(a => a.calificacion);

            }
            return Convert.ToInt32(promedio);
        }
        private string cargaPrecio(Precios precio)
        {
            if (precio.descuentoActivo && precio.fechaInicio < DateTime.Now && precio.fechaVencimiento > DateTime.Now)
            {
                return precio.precioEsp.ToString("#.##") + " MXN";
            }
            else
            {
                return precio.precio.ToString("#.##") + " MXN";
            }
        }

        public ActionResult ListaCategorias()
        {
            List<CategoriasMenuRapido> menu = new List<CategoriasMenuRapido>();
            foreach (var item in _categorias.Cargar(a => a.idPadre == 0))
            {
                menu.Add(new CategoriasMenuRapido
                {
                    idCategoria = item.idCategoria,
                    nombreCategoria = item.nombreCategoria
                });
            }
            return View("_listaCategorias", menu);
        }
        public ActionResult CargaHijo(int id) {
            List<CategoriasMenuRapido> menu = new List<CategoriasMenuRapido>();
            foreach (var item in _categorias.Cargar(a => a.idPadre == id))
            {
                menu.Add(new CategoriasMenuRapido
                {
                    idCategoria = item.idCategoria,
                    nombreCategoria = item.nombreCategoria
                });
            }
            return Json(menu,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContenidoCabecera()
        {
            var empresa = _empresa.Cargar(a => a.infoActiva == true).SingleOrDefault();
            Empresa model = new Empresa();
            if (empresa != null)
            {
                model = empresa;

            }
            return View("_contenidoCabeceraEshopper", model);
        }
        public ActionResult ContenidoPiePagina()
        {
            return View();
        }
        //Se encarga de cargar el tema predefinido
        public ActionResult Head()
        {
            return View();
        }
        public ActionResult Footer()
        {
            var empresa = _empresa.Cargar(a => a.infoActiva == true).SingleOrDefault();
            Empresa model = new Empresa();
            if (empresa != null)
            {
                model = empresa;

            }
            return View("_contenidoPieEshopper", model);
        }
        public ActionResult ScriptsSociales()
        {
            return View();
        }
    }
}