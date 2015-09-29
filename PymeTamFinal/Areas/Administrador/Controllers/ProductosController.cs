using PymeTamFinal.Attributos;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class ProductosController : AdminController
    {
        #region props
        IRepositorioBase<Producto> _producto;
        IRepositorioBase<Categoria> _categoria;
        IRepositorioBase<GaleriaProducto> _galeria;
        IRepositorioBase<Precios> _precios;
        public const string carpetaProductos = "/Content/Imagenes/Productos/";
        public const string carpetaGenericas = "/Content/Imagenes/Genericas/";
        #endregion
        #region ctor
        public ProductosController(IRepositorioBase<Producto> _producto, IRepositorioBase<Categoria> _categoria, IRepositorioBase<GaleriaProducto> _galeria, IRepositorioBase<Precios> _precios)
        {
            this._producto = _producto;
            this._categoria = _categoria;
            this._galeria = _galeria;
            this._precios = _precios;
        }
        #endregion
        #region FunFunFun
        // GET: Administrador/Productos
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            var productos = _producto.Cargar();
            return View(productos);
        }
        public ActionResult CargaProductos()
        {
            var productos = _producto.Cargar();
            var lista = new List<ProductoTablaViewModel>();
            lista = cargaModelo(productos);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        private List<ProductoTablaViewModel> cargaModelo(IQueryable<Producto> productos)
        {
            var lista = new List<ProductoTablaViewModel>();
            foreach (var item in productos)
            {
                var productoTablaModel = new ProductoTablaViewModel()
                {
                    descripcionCorta = item.descripcionCorta,
                    descripcionProducto = item.descripcionProducto,
                    fechaModificacion = item.fechaModificacion.ToShortDateString(),
                    habilitado = item.habilitado,
                    habilitarComentarios = item.habilitarComentarios,
                    categoria = generarCategoria(item.categoria),
                    idProducto = item.idProducto,
                    imgProducto = item.imgProducto,
                    inhabilitarCompraSinStock = item.habilitarCompraSinStock,
                    mostrarSinStock = item.mostrarSinStock,
                    mostrarStock = item.mostrarStock,
                    nombreProducto = item.nombreProducto,
                    slugs = item.slugs,
                    stock = item.stock,
                    tags = item.tags,
                    opciones = generarOpciones(item.idProducto),
                    sku = item.sku,
                    fechaCreacion = item.fechaCreacion,
                    precio = cargaPrecio(item.idProducto)
                };
                lista.Add(productoTablaModel);

            }
            return lista;
        }
        public ActionResult CargaPaginadas()
        {
            var echo = int.Parse(Request.Params["sEcho"]);
            var displayLength = int.Parse(Request.Params["iDisplayLength"]);
            var displayStart = int.Parse(Request.Params["iDisplayStart"]);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarProducto()
        {
            ViewBag.categorias = cargaCategorias;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AgregarProducto(Producto model, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                model.fechaCreacion = DateTime.Now;
                model.fechaModificacion = DateTime.Now;

                model.slugs = PlugIns.AdministradorTexto.GeneradorSlugs(model.nombreProducto);
                _producto.Agregar(model);
                asignarImagenPrecio(model, imagen);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            ViewBag.categorias = cargaCategorias;
            return View(model);
        }
        //[AdminAutorizacionParcialAttr(Roles = "Administrador")]
        public ActionResult AdministrarPrecio(int id)
        {
            var producto = _producto.CargarPorId(id);
            return View(producto);
        }
        public ActionResult EditarProducto(int? id)
        {
            if (!id.HasValue)
                return HttpNotFound();
            var model = _producto.CargarPorId(id);
            if (model == null)
                return HttpNotFound();
            ViewBag.categorias = cargaCategorias;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditarProducto(Producto model, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null)
                {
                    PlugIns.AdministradorDeArchivos.eliminarArchivo(model.imgProducto);
                    model.imgProducto = PlugIns.AdministradorDeArchivos.guardarArchivo(imagen, carpetaProductos, model.idProducto.ToString());
                }
                model.fechaModificacion = DateTime.Now;
                model.slugs = PlugIns.AdministradorTexto.GeneradorSlugs(model.nombreProducto);
                _producto.Editar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            ViewBag.categorias = cargaCategorias;
            return View(model);
        }
        public ActionResult EditarGaleria(int id)
        {
            var modelo = _producto.CargarPorId(id);
            if (modelo != null)
            {
                Session["id"] = id;
                return View(modelo);
            }
            return HttpNotFound();
        }
        //[AdminAutorizacionParcialAttr(Roles = "Administrador")]
        public ActionResult EditarPrecio(int id)
        {
            ViewBag.tipos = cargaTipos;
            ViewBag.idProducto = id;
            var precio = _precios.CargarPorId(id);
            return View(precio);
        }
        [HttpPost]
        public ActionResult EditarPrecio(Precios model)
        {
            if (ModelState.IsValid)
            {
                if (model.descuento.HasValue && model.descuentoActivo) {
                    switch (model.tipo)
                    {
                        case "Porcentual":
                            model.precioEsp = (model.precio * model.descuento.Value) / 100;
                            break;
                        case "Real":
                            model.precioEsp = model.precio - model.descuento.Value;
                            break;
                        default:
                            break;
                    }
                }
                var precioOrigial = cargaOriginal(model.idProducto);
                if (precioOrigial != null)
                {
                    //Eliminamos el objeto de la memoria
                    _precios.DesAsociar(precioOrigial);
                    model.idPrecio = precioOrigial.idPrecio;
                    _precios.Editar(model);
                    ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                }
                else
                {
                    _precios.Agregar(model);
                    ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado, ControllerContext.Controller);
                }
            }

            return RedirectToAction("Index");
        }
        [AdminAutorizacionParcialAttr(Roles = "Administrador")]
        public ActionResult EliminarProductoVentana(int? id) {
            if (!id.HasValue) {
                if (!Request.IsAjaxRequest()) {
                    return HttpNotFound();
                }
                return View(error404Parcial);
            }
            var producto = _producto.CargarPorId(id);
            return View(producto);
        }
        [Authorize(Roles ="Administrador")]
        public ActionResult EliminarProducto(int id) {
            
            var producto = _producto.CargarPorId(id);
            int idProducto = producto.idProducto;
            eliminarGaleria(idProducto);
            eliminarPrecio(idProducto);
            _producto.Eliminar(producto);
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Eliminado,ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        private void eliminarPrecio(int idProducto)
        {
            var precio = _precios.CargarPorId(idProducto);
            if (precio != null) {
                _precios.Eliminar(precio);
            }
            return;
        }
        [HttpPost]
        public ActionResult SubirImagen()
        {

            if (Session["id"] == null)
            {
                return Json(new
                {
                    statusCode = 500,
                    status = "La sesión ha terminado, recargue la pagina para continuar",
                    file = string.Empty
                });
            }
            var id = Convert.ToInt32(Session["id"]);
            if (Request.Files.Count == 0)
            {
                return Json(new { statusCode = 500, status = "El archivo no fue reconocido." }, "text/html");
            }
            try
            {
                generarGaleria(Request, id);

                return Json(new
                {
                    statusCode = 200,
                    status = "Imagen Guardada.",
                    file = Request.Files[0].FileName,
                }, "text/html");
            }
            catch (Exception)
            {
                return Json(new
                {
                    statusCode = 500,
                    status = "Error al subir la imagen.",
                    file = string.Empty
                }, "text/html");
            }
        }
        public ActionResult ImagenesGaleria(int id)
        {
            var galeria = _galeria.Cargar(a => a.idProducto == id);
            return View(galeria);
        }
        public ActionResult EliminaImagen(int id)
        {
            var imagen = _galeria.CargarPorId(id);
            PlugIns.AdministradorDeArchivos.eliminarArchivo(imagen.imagen);
            _galeria.Eliminar(imagen);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region helpers
        private void asignarImagenPrecio(Producto model, HttpPostedFileBase imagen)
        {
            //var precio = new Precios()
            //{
            //    descuento = 0,
            //    descuentoActivo = false,
            //    fechaInicio = null,
            //    fechaVencimiento = null,
            //    idProducto = model.idProducto
            //};
            //_precios.Agregar(precio);
            if (imagen != null)
            {
                model.imgProducto = PlugIns.AdministradorDeArchivos.guardarArchivo(imagen, carpetaProductos, model.idProducto.ToString());
            }
            _producto.Editar(model);
        }
        private Precios cargaOriginal(int idProducto)
        {
           return _precios.CargarPorId(idProducto);
        }
        private void generarGaleria(HttpRequestBase request, int id)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                //Guardamos las otras imagenes
                var imagen = Request.Files[i];
                var ruta = PlugIns.AdministradorDeArchivos.guardarArchivo(imagen, carpetaProductos, id.ToString() + "/Slides");
                var gal = new GaleriaProducto()
                {
                    imagen = ruta,
                    fechaRegistro = DateTime.Now,
                    idProducto = id
                };

                _galeria.Agregar(gal);
            }
        }
        private string generarCategoria(Categoria categoria)
        {
            TagBuilder span = new TagBuilder("span");
            if (categoria != null)
            {
                span.MergeAttribute("class", "label label-success");
                span.SetInnerText(categoria.nombreCategoria);
            }
            else
            {
                span.MergeAttribute("class", "label label-danger");
                span.SetInnerText("No categorizado");
            }
            return span.ToString();
        }
        private string generarOpciones(int idProducto)
        {
            Dictionary<string, string> attr = new Dictionary<string, string>();

            attr.Add("data-id", idProducto.ToString());

            TagBuilder aEditar = new TagBuilder("a");
            aEditar.MergeAttributes(attr);
            aEditar.MergeAttribute("href", Url.Action("EditarProducto", "Productos", new { id = idProducto }));
            aEditar.SetInnerText("Editar");
            TagBuilder aEliminar = new TagBuilder("a");
            aEliminar.MergeAttributes(attr);
            aEliminar.MergeAttribute("class", "eliminarProducto");
            aEliminar.MergeAttribute("href", "#");
            aEliminar.SetInnerText("Eliminar");
            TagBuilder aGaleria = new TagBuilder("a");
            aGaleria.MergeAttributes(attr);
            aGaleria.MergeAttribute("class", "editarGaleria");
            aGaleria.MergeAttribute("href", "#");
            aGaleria.SetInnerText("Galeria");
            TagBuilder aPrecio = new TagBuilder("a");
            aPrecio.MergeAttributes(attr);
            aPrecio.MergeAttribute("class", "editarPrecio");
            aPrecio.MergeAttribute("href", "#");
            aPrecio.SetInnerText("Precio");
            var opt = aEditar.ToString() + " | " + aGaleria.ToString() + " | " + aPrecio.ToString() + " | " + aEliminar.ToString();
            return opt;
        }
        private List<Categoria> listaCategorias = new List<Categoria>();
        private List<tipo> tipos = new List<tipo>();
        public List<Categoria> categorias()
        {
            var cat = _categoria.Cargar(a => a.idPadre == 0);
            foreach (var categoria in cat)
            {
                listaCategorias.Add(new Categoria
                {
                    idCategoria = categoria.idCategoria,
                    idPadre = categoria.idPadre,
                    nombreCategoria = categoria.nombreCategoria
                });
                obtenerHijos(categoria);
            }
            return listaCategorias;
        }
        private void obtenerHijos(Categoria categoria)
        {
            var hijos = _categoria.Cargar(a => a.idPadre == categoria.idCategoria);
            foreach (var hijo in hijos)
            {
                listaCategorias.Add(new Categoria
                {
                    idCategoria = hijo.idCategoria,
                    idPadre = hijo.idPadre,
                    nombreCategoria = "--" + hijo.nombreCategoria,
                });
                obtenerHijos(hijo);
            }
        }
        private string cargaPrecio(int idProducto)
        {
            var precio = _precios.CargarPorId(idProducto);
            if (precio != null)
            {
                string p = precio.precio.ToString("#.##");
                if (precio.descuentoActivo)
                {
                    //Ahora chequemos si el precio esta en fecha
                    if (precioEnFecha(precio.fechaInicio, precio.fechaVencimiento)) {
                        p += " - " + precio.precioEsp.ToString("#.##") + " Descuento activo.";
                    }
                }
                return p;
            }
            return "precio no disponible";
        }
        private void eliminarGaleria(int idProducto)
        {
            var galeria = _galeria.Cargar(a => a.idProducto == idProducto);
            foreach (var item in galeria.ToList())
            {
                PlugIns.AdministradorDeArchivos.eliminarArchivo(item.imagen);
                _galeria.Eliminar(item);
            }
        }
        private bool precioEnFecha(DateTime? fechaInicio, DateTime? fechaVencimiento)
        {
            if (fechaInicio.HasValue && fechaVencimiento.HasValue)
            {
                var fechaActual = DateTime.Now;
                //Ya que revisamos que los 2 tengan valores comparamos
                if (fechaActual > fechaInicio)
                {
                    //Pasamos el primer filtro
                    if (fechaActual < fechaVencimiento)
                    {
                        //La fecha de vencimiento no ha pasado
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    //Aun no esta en fecha
                    return false;
                }
            }
            else {
                return false;
            }
        }
        private SelectList cargaCategorias
        {
            get { return new SelectList(categorias(), "idCategoria", "nombreCategoria"); }
        }
        private SelectList cargaTipos
        {
            get
            {
                tipos.Add(new tipo
                {
                    nombre = "Real",

                });
                tipos.Add(
                new tipo
                {
                    nombre = "Porcentual"
                });
                return new SelectList(tipos, "nombre", "nombre");

            }
        }
        private class tipo
        {
            int id { get; set; }
            public string nombre { get; set; }
        }
        #endregion
    }
}