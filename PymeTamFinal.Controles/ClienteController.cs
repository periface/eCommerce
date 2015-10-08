using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Controles
{
    public class ClienteController : Controller
    {
        public ViewResult error404Tienda
        {
            get
            {
                return View("_error404Tienda");
            }
        }
        /// <summary>
        /// Helpers
        /// </summary>
        public SelectList paises {
            get {
                IRepositorioBase<Pais> _paises = new Repositorios.Repos.RepositorioPaises(new DataContext());
                return new SelectList(_paises.Cargar(),"idPais", "nombrePais");
            }
        }
        public SelectList estados
        {
            get
            {
                IRepositorioBase<Estados> _estados = new Repositorios.Repos.RepositorioEstados(new DataContext());
                return new SelectList(_estados.Cargar(), "idEstado", "nombreEstado");
            }
        }
        public JsonResult cargaEstadosPorId(int idPais) {
            IRepositorioBase<Estados> _estados= new Repositorios.Repos.RepositorioEstados(new DataContext());
            List<Estados> estados = new List<Estados>();
            foreach (var item in _estados.Cargar(a => a.idPais == idPais))
            {
                estados.Add(new Estados() {
                    idEstado = item.idEstado,
                    nombreEstado = item.nombreEstado
                });
            }
            return Json(estados,JsonRequestBehavior.AllowGet);
        }
    }
}
