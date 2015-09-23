using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using PymeTamFinal.Modelos.ModelosVista;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
using PymeTamFinal.Attributos;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class CategoriasController : Controller
    {
        IRepositorioBase<Categoria> _categorias;
        
        public CategoriasController(IRepositorioBase<Categoria> _categorias)
        {
            this._categorias = _categorias;
        }
        // GET: Administrador/Categorias
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
            var categorias = _categorias.Cargar();
            return View(categorias);
        }
        public ActionResult NuevaCategoria()
        {
            ViewBag.categorias = categorias();
            return View();
        }
        [HttpPost]
        public ActionResult NuevaCategoria(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _categorias.Agregar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado,ControllerContext.Controller);
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditarCategoria(int id)
        {
            ViewBag.categorias = cargaCategorias;
            var categoria = _categorias.CargarPorId(id);

            return View(categoria);
        }
        [HttpPost]
        public ActionResult EditarCategoria(Categoria model)
        {
            if (ModelState.IsValid)
            {
                var checkbox = Request.Form["padre"];
                if (checkbox == "on")
                {
                    model.idPadre = 0;
                    if (model.idCategoria != 0)
                    {
                        ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);

                        //Si el modelo es un hijo editamos para que sea padre
                        _categorias.Editar(model);
                    }
                    else
                    {
                        ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                        //Guardar como nuevo padre ignorar edicion
                        _categorias.Agregar(model);

                    }
                }
                if (model.idCategoria == model.idPadre)
                {
                    ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.ErrorRecurrencia, ControllerContext.Controller);
                    return RedirectToAction("Index");
                }
                if (model.idCategoria != 0)
                {
                    ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                    _categorias.Editar(model);
                }
                else
                {
                    ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Editado, ControllerContext.Controller);
                    _categorias.Agregar(model);
                }
                return RedirectToAction("Index");

            }
            return View(model);
        }
        public ActionResult ArbolCategorias()
        {
            var padres = _categorias.Cargar(a => a.idPadre == 0);
            return View(padres);
        }
        [AdminAutorizacionParcialAttr(Roles = "Administrador")]
        public ActionResult EliminarVentana(int id) {
            CategoriaAdminEliminaViewModel model = new CategoriaAdminEliminaViewModel();
            var categoria = _categorias.CargarPorId(id);
            model.categoria.idCategoria = categoria.idCategoria;
            model.categoria.idPadre = categoria.idPadre;
            model.categoria.nombreCategoria = categoria.nombreCategoria;
            var categoriasDependientes = _categorias.Cargar(a=>a.idPadre==id);
            foreach (var item in categoriasDependientes)
            {
                model.hijos.Add(new CategoriaAdminViewModel() {
                    idCategoria = item.idCategoria,
                    idPadre = item.idPadre,
                    nombreCategoria = item.nombreCategoria
                });
            }
            return View(model);
        }
        public ActionResult EliminarCategoria(int id) {
            var categoria = _categorias.CargarPorId(id);
            _categorias.Eliminar(categoria);
            ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Eliminado,ControllerContext.Controller);
            return RedirectToAction("Index");
        }
        public ActionResult CargaHijos(int id)
        {
            var hijos = _categorias.Cargar(a => a.idPadre == id);
            return Json(hijos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PromocionCategoria(int idCategoria) {
            var categoria = _categorias.CargarPorId(idCategoria);
            return View(categoria);
        }
        public ActionResult PromocionCategoria(decimal descuento,bool difusión,string mensajeDifusion,HttpPostedFileBase imagenDifusion,bool difusionPCorreo,int idCategoria) {
            return View();
        }
        #region helpers
        private List<CategoriaAdminViewModel> listaCategorias = new List<CategoriaAdminViewModel>();
        public List<CategoriaAdminViewModel> categorias()
        {
            var cat = _categorias.Cargar(a => a.idPadre == 0);
            foreach (var categoria in cat)
            {
                listaCategorias.Add(new CategoriaAdminViewModel
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
            var hijos = _categorias.Cargar(a => a.idPadre == categoria.idCategoria);
            foreach (var hijo in hijos)
            {
                listaCategorias.Add(new CategoriaAdminViewModel
                {
                    idCategoria = hijo.idCategoria,
                    idPadre = hijo.idPadre,
                    nombreCategoria = "--" + hijo.nombreCategoria,
                });
                obtenerHijos(hijo);
            }
        }
        private SelectList cargaCategorias
        {
            get { return new SelectList(categorias(), "idCategoria", "nombreCategoria"); }
        }
        #endregion

    }
}