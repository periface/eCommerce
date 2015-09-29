﻿using PagedList;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class TiendaController : ClienteController
    {
        IRepositorioBase<Producto> _productos;
        IRepositorioBase<Categoria> _categorias;
        IRepositorioBase<Precios> _precios;
        IRepositorioBase<CajaComentarios> _comentarios;

        public TiendaController(IRepositorioBase<Producto> _productos, IRepositorioBase<Categoria> _categorias, IRepositorioBase<CajaComentarios> _comentarios, IRepositorioBase<Precios> _precios)
        {
            this._productos = _productos;
            this._categorias = _categorias;
            this._comentarios = _comentarios;
            this._precios = _precios;
        }
        private List<ProductoListaViewModel> _productosLst = new List<ProductoListaViewModel>();
        // Inicio de la tienda
        public ActionResult Index()
        {
            return View();
        }
        [Route("DetalleProducto/{id}/{slug}")]
        public ActionResult DetalleProducto(int? id, string slug)
        {
            if (!id.HasValue)
                return error404Tienda;
            var producto = _productos.Cargar(a=>a.idProducto==id && a.slugs == slug).SingleOrDefault();
            if (producto == null)
                return error404Tienda;
            var precio = _precios.CargarPorId(id);
            if (precio == null) {
                return error404Tienda;
            }
            ProductoDetalleViewModel model = new ProductoDetalleViewModel() {
                imagen = producto.imgProducto,
                cajaComentarios = producto.habilitarComentarios,
                idProducto = producto.idProducto,
                descripcionCorta = producto.descripcionCorta,
                disponibleSinStock = producto.habilitarCompraSinStock,
                detalle = producto.descripcionProducto,
                mostrarStock = producto.mostrarStock,
                precio = cargaPrecio(precio),
                promedio = cargaProm(producto.idProducto),
                slug = producto.slugs,
                stock = producto.stock,
                tags = producto.tags,
                totalComents = 0,
                nombreProducto = producto.nombreProducto,
                urlImg = PlugIns.PlugInUrl.ResolveServerUrl(producto.imgProducto,false),
                
            };
            return View(model);
        }
        public ActionResult Productos(int? idCategoria, int? pagina, string orden, string busquedaString, string busqueda, int? min, int? max)
        {
            var productos = _productos.Cargar(a => a.stock > 0 || a.mostrarSinStock == true && a.habilitado == true);
            PaginaProductosViewModel model = new PaginaProductosViewModel();
            model = cargaProductos(idCategoria, pagina, orden, busquedaString, busqueda, min, max);
            //model._categorias = cargaCategorias();
            return View(model);
        }

        private List<CategoriasMenuRapido> cargaCategorias()
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
            return menu;
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
        private decimal cargaPrecio(Precios precio)
        {
            if (precio.descuentoActivo && precio.fechaInicio < DateTime.Now && precio.fechaVencimiento > DateTime.Now)
            {
                return precio.precioEsp;
            }
            else
            {
                return precio.precio;
            }
        }
        private PaginaProductosViewModel cargaProductos(int? idCategoria, int? pagina, string orden, string busquedaString, string busqueda, int? min, int? max)
        {
            int pageSize = 9;
            int pageNumber = (pagina ?? 1);
            var listaCategorias = new List<CategoriasMenuRapido>();
            bool multipleEncontrado = false;
            PaginaProductosViewModel model = new PaginaProductosViewModel();
            IEnumerable<Producto> _productosDb = _productos.Cargar(a => a.habilitado == true);
            List<ProductoListaViewModel> productoConversion = new List<ProductoListaViewModel>();
            foreach (var item in _productosDb)
            {
                var precio = _precios.CargarPorId(item.idProducto);
                if (precio != null)
                {
                    productoConversion.Add(new ProductoListaViewModel()
                    {
                        calificacionProm = 0,
                        descCorta = item.descripcionCorta,
                        idProducto = item.idProducto,
                        imagen = item.imgProducto,
                        nombreProducto = item.nombreProducto,
                        precio = cargaPrecio(precio),
                        slug = item.slugs,
                        idCategoria = item.idCategoria.HasValue ? item.idCategoria.Value : 0,
                        descLarga = item.descripcionProducto,
                        fechaCreacion = item.fechaCreacion
                    });
                }
            }
            var _max = productoConversion.OrderByDescending(a => a.precio).Any() ? productoConversion.OrderByDescending(a => a.precio).First().precio : 0;
            var _min = productoConversion.OrderBy(a => a.precio).Any() ? productoConversion.OrderBy(a => a.precio).First().precio : 0;
            //PlugInAutomapper<CapaDatos.catCategoria, categorias> plugin = new PlugInAutomapper<CapaDatos.catCategoria, categorias>();
            if (idCategoria.HasValue)
            {
                var cat = _categorias.Cargar(a => a.idPadre == idCategoria.Value);
                var padre = _categorias.Cargar(a => a.idCategoria == idCategoria.Value).SingleOrDefault();
                if (padre != null)
                {

                    var nom = padre.nombreCategoria;
                    ViewBag.nombre = nom;
                }
                foreach (var item in cat)
                {
                    listaCategorias.Add(new CategoriasMenuRapido()
                    {
                        idCategoria = item.idCategoria,
                        nombreCategoria = item.nombreCategoria
                    });
                }
                model._categorias = listaCategorias;

            }
            else
            {
                foreach (var item in cargaCategorias())
                {
                    listaCategorias.Add(item);
                }
                model._categorias = listaCategorias;
            }
            if (idCategoria.HasValue)
            {
                ViewBag.categoriaActual = idCategoria.Value;
                productoConversion = productoConversion.Where(a => a.idCategoria == idCategoria).ToList();
                ///Carga productos de todos los hijos de la categoria
                ///Recursividad de un solo nivel, posiblemente se deba crear un metodo que se llame a si mesmo
                foreach (var item in _categorias.Cargar(a => a.idPadre == idCategoria))
                {
                    cargaProductos(item);

                }
                foreach (var producto in _productosLst)
                {
                    model._productos.Add(producto);
                }
                if (model._productos.Any())
                {
                    multipleEncontrado = true;
                }
                if (busquedaString != null)
                {
                    pagina = 1;
                }
                else
                {
                    busquedaString = busqueda;
                }
                ViewBag.filtroActual = busquedaString;
                ViewBag.ordenNombre = string.IsNullOrEmpty(orden) ? "Nombre" : "";
                ViewBag.ordenPrecio = orden == "Precio" ? "Nombre" : "Precio";
                if (!string.IsNullOrEmpty(busquedaString))
                {
                    productoConversion = productoConversion.Where(a => a.nombreProducto.ToUpper().Contains(busquedaString.ToUpper()) || a.descCorta.ToUpper().Contains(busquedaString.ToUpper()) || a.descLarga.ToUpper().Contains(busquedaString.ToUpper())).ToList();
                }
                switch (orden)
                {
                    case "Nombre":
                        productoConversion = productoConversion.OrderBy(a => a.nombreProducto).ToList();
                        ViewBag.ordenActual = "Nombre";
                        break;
                    case "Precio":
                        productoConversion = productoConversion.OrderByDescending(a => a.precio).ToList();
                        ViewBag.ordenActual = "Precio";
                        break;
                    default:
                        productoConversion = productoConversion.OrderBy(a => a.nombreProducto).ToList();
                        break;
                }
                if (min.HasValue && max.HasValue)
                {
                    ViewBag.actMax = max.Value;
                    ViewBag.actMin = min.Value;
                    productoConversion = productoConversion.Where(a => a.precio >= min.Value && a.precio <= max.Value).ToList();
                }
                else
                {
                    ViewBag.actMax = productoConversion.OrderByDescending(a => a.precio).Any() ? productoConversion.OrderByDescending(a => a.precio).First().precio : 0;
                    ViewBag.actMin = productoConversion.OrderBy(a => a.precio).Any() ? productoConversion.OrderBy(a => a.precio).First().precio : 0;
                }
                ViewBag.max = productoConversion.OrderByDescending(a => a.precio).Any() ? productoConversion.OrderByDescending(a => a.precio).First().precio : 0;
                ViewBag.min = productoConversion.OrderBy(a => a.precio).Any() ? productoConversion.OrderBy(a => a.precio).First().precio : 0;

                model._productos.AddRange(productoConversion);
                if (multipleEncontrado)
                {
                    if (!min.HasValue && !max.HasValue)
                    {
                        ViewBag.actMax = model._productos.OrderByDescending(a => a.precio).Any() ? model._productos.OrderByDescending(a => a.precio).First().precio : 0; ;
                        ViewBag.actMin = model._productos.OrderBy(a => a.precio).Any() ? model._productos.OrderBy(a => a.precio).First().precio : 0; ;
                    }
                    ViewBag.max = model._productos.OrderByDescending(a => a.precio).Any() ? model._productos.OrderByDescending(a => a.precio).First().precio : 0;
                    ViewBag.min = model._productos.OrderBy(a => a.precio).Any() ? model._productos.OrderBy(a => a.precio).First().precio : 0;
                }
                model._productos.ToPagedList(pageNumber, pageSize).ToList();
                return model;
            }
            else
            {
                if (busquedaString != null)
                {
                    pagina = 1;
                }
                else
                {
                    busquedaString = busqueda;
                }
                ViewBag.filtroActual = busquedaString;
                ViewBag.ordenNombre = string.IsNullOrEmpty(orden) ? "Nombre" : "";
                ViewBag.ordenPrecio = orden == "Precio" ? "Nombre" : "Precio";
                if (!string.IsNullOrEmpty(busquedaString))
                {
                    model._productos = model._productos.Where(a => a.nombreProducto.ToUpper().Contains(busquedaString.ToUpper()) || a.descCorta.ToUpper().Contains(busquedaString.ToUpper()) || a.descLarga.ToUpper().Contains(busquedaString.ToUpper())).ToList();
                }
                switch (orden)
                {
                    case "Nombre":
                        productoConversion = productoConversion.OrderBy(a => a.nombreProducto).ToList();
                        ViewBag.ordenActual = "Nombre";
                        break;
                    case "Precio":
                        ViewBag.ordenActual = "Precio";
                        productoConversion = productoConversion.OrderByDescending(a => a.precio).ToList();
                        break;
                    default:
                        productoConversion = productoConversion.OrderBy(a => a.fechaCreacion).ToList();
                        break;
                }
                if (min.HasValue && max.HasValue)
                {
                    ViewBag.actMax = max.Value;
                    ViewBag.actMin = min.Value;
                    productoConversion = productoConversion.Where(a => a.precio >= min.Value && a.precio <= max.Value).ToList();
                }
                else
                {
                    ViewBag.actMax = _max;
                    ViewBag.actMin = _min;
                }
                ViewBag.max = _max;
                ViewBag.min = _min;
                model._productos.AddRange(productoConversion);
                return model;
            }
        }

        private void cargaProductos(Categoria item)
        {
            foreach (var p in _productos.Cargar(a => a.idCategoria == item.idCategoria))
            {
                var precio = _precios.CargarPorId(p.idProducto);
                if (precio != null)
                {
                    _productosLst.Add(new ProductoListaViewModel
                    {
                        calificacionProm = cargaProm(p.idProducto),
                        descCorta = p.descripcionCorta,
                        fechaCreacion = p.fechaCreacion,
                        descLarga = p.descripcionProducto,
                        idCategoria = p.idCategoria.HasValue ? p.idCategoria.Value : 0,
                        idProducto = p.idProducto,
                        imagen = p.imgProducto,
                        nombreProducto = p.nombreProducto,
                        precio = cargaPrecio(precio),
                        slug = p.slugs
                    });
                }

            }
            foreach (var cat in _categorias.Cargar(a => a.idPadre == item.idCategoria))
            {
                cargaProductos(cat);
            }
        }

        public ActionResult Ayuda()
        {
            return View();
        }
    }
}