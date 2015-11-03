using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PedidosController : AdminController
    {
        IRepositorioBase<Orden> _orden;
        IGeneradorGraficas<Orden> _ordenGraficas;
        IGeneradorDataTables<TablaPedidosViewModel, Orden> _datatableOrden;
        //IGeneradorGraficas<GraficaPastel> _estados;
        public PedidosController(IRepositorioBase<Orden> _orden, IGeneradorGraficas<Orden> _ordenGraficas, IGeneradorDataTables<TablaPedidosViewModel, Orden> _datatableOrden)
        {
            this._orden = _orden;
            this._ordenGraficas = _ordenGraficas;
            this._datatableOrden = _datatableOrden;
        }
        // GET: Administrador/Pedidos
        public ActionResult Index()
        {
            return View(_orden.Cargar());
        }
        public ActionResult AgregarCodigoRastreo(int? id)
        {
            return View();
        }
        public ActionResult MarcarComoPagado(int? id)
        {
            return View();
        }
        public ActionResult CancelarPedido(int? id)
        {
            return View();
        }
        public ActionResult EditarEstado(int? id)
        {
            return View();
        }
        public ActionResult VerDetalles(int? id)
        {
            return View();
        }
        public ActionResult InvalidarTicket(int? id)
        {
            return View();
        }
        public ActionResult ValidarTicket(int? id)
        {
            return View();
        }
        public JsonResult VolumenVentas()
        {
            var pedidos = _orden.Cargar();
            var graficas = _ordenGraficas.generarGrafica(pedidos);
            return Json(graficas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MediosFiltro(string medio)
        {
            var medios = _orden.Cargar(a => a.ordenTipoPago.Equals(medio));
            return View(medios);
        }
        public ActionResult Estados()
        {
            var estados = _ordenGraficas.generarGraficaAgrupacion("ordenEstadoPedido");
            return Json(estados, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TiposPago()
        {
            var estados = _ordenGraficas.generarGraficaAgrupacion();
            return Json(estados, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PedidosJson(JQueryDataTableParamModel param)
        {
            //Generamos los parametros

            var columnaId = Convert.ToInt32(Request["iSortCol_0"]);
            var columnaIdDireccion = Request["sSortDir_0"];

            //Generamos el modelo para la tabla, filtrado si hay parametros de busqueda

            var pedidos = _datatableOrden.modeloTabla(_orden.Cargar(), param.sSearch);

            //Paginamos los resultados

            IEnumerable<TablaPedidosViewModel> pedidosMostrados = _datatableOrden.PaginarResultados(param.iDisplayStart, param.iDisplayLength, pedidos);

            //Ordenamos los resultados

            pedidosMostrados = _datatableOrden.OrdenaResultados(pedidosMostrados, columnaId, columnaIdDireccion);

            //Generamos la lista de arreglos que corresponda al numero de columnas

            List<string[]> lista = _datatableOrden.generarListaDeArreglo(pedidosMostrados);
            JQueryDataTableResponseModel model = new JQueryDataTableResponseModel()
            {
                sEcho = param.sEcho,
                
                iTotalRecords = pedidos.Count,
                iTotalDisplayRecords = pedidos.Count,
                aaData = lista
            };
            //Regresamos los datos procesados

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #region Refactorizado
        //Refactorizar
        //Extraer interfaces
        //private List<string[]> generaListaDeArreglo(IEnumerable<TablaPedidosViewModel> pedidosMostrados)
        //{
        //    var lista = new List<string[]>();
        //    foreach (var item in pedidosMostrados)
        //    {
        //        lista.Add(new string[] {
        //            item.idOrden.ToString(),item.cliente,item.ordenFecha,item.ordenHora,item.ordenTotal,item.ordenEstadoPedido,item.ordenTipoPago,item.ordenPagado,item.ordenDetalle
        //        });
        //    }
        //    return lista;
        //}

        //private IEnumerable<TablaPedidosViewModel> OrdenaResultados(IEnumerable<TablaPedidosViewModel> pedidosMostrados, int columnaId, string columnaIdDireccion)
        //{
        //    Func<TablaPedidosViewModel, string> funcionOrdenamiento = (c => columnaId == 0 ? c.idOrden.ToString() : columnaId == 1 ? c.cliente : columnaId == 2 ? c.ordenFecha : columnaId == 3 ? c.ordenHora : columnaId == 4 ? c.ordenTotal : columnaId == 5 ? c.ordenTipoPago : c.ordenEstadoPedido);
        //    if (columnaIdDireccion == "asc")
        //    {
        //        pedidosMostrados = pedidosMostrados.OrderBy(funcionOrdenamiento);
        //    }
        //    else
        //    {
        //        pedidosMostrados = pedidosMostrados.OrderByDescending(funcionOrdenamiento);
        //    }
        //    return pedidosMostrados;
        //}

        //private IEnumerable<TablaPedidosViewModel> PaginarResultados(int inicio, int toma, IEnumerable<TablaPedidosViewModel> pedidos)
        //{
        //    return pedidos.Skip(inicio).Take(toma);
        //}

        ////private List< Dictionary<string, int>> generarDiccionario(IQueryable<Orden> pedidos)
        ////{
        ////    var lista = pedidos.ToList();
        ////    var dic = new List<Dictionary<string, int>>();

        ////    for (int i = 0; i < lista.Count; i++)
        ////    {
        ////        int val1 = Convert.ToInt16(lista[i].ordenTotal);
        ////        string val2 = lista[i].ordenFecha.ToShortDateString();
        ////        var valores = new Dictionary<string, int>();
        ////        valores.Add(val2,val1);
        ////        dic.Add(valores);
        ////    }
        ////    return dic;
        ////}
        //private List<TablaPedidosViewModel> modeloTabla(IQueryable<Orden> pedidos, string busqueda)
        //{
        //    List<TablaPedidosViewModel> pedidosLista = new List<TablaPedidosViewModel>();
        //    foreach (var item in pedidos)
        //    {
        //        pedidosLista.Add(new TablaPedidosViewModel()
        //        {

        //            idOrden = item.idOrden,
        //            ordenDetalle = generarBoton(item),
        //            ordenEstadoPedido = item.ordenEstadoPedido,
        //            ordenFecha = item.ordenFecha.ToShortDateString(),
        //            ordenHora = item.ordenFecha.ToShortTimeString(),
        //            ordenPagado = item.ordenPagado == true ? "Pagado" : "Pendiente de pago",
        //            ordenTipoPago = item.ordenTipoPago,
        //            ordenTotal = item.ordenTotal.ToString("#.##"),
        //            cliente = item.cliente.nombreCliente
        //        });
        //    }
        //    if (!string.IsNullOrEmpty(busqueda))
        //    {
        //        pedidosLista = pedidosLista.Where(a => a.idOrden.ToString().Contains(busqueda)).ToList();
        //    }
        //    return pedidosLista;
        //}
        //private string generarBoton(Orden item)
        //{
        //    var url = string.Format("<a href='{0}'>Detalle</a>", Url.Action("Detalle", "Pedidos", new { id = item.idOrden }));
        //    return url;
        //} 
        #endregion
    }
}