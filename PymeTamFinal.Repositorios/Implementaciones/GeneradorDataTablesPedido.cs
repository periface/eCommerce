using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    public class GeneradorDataTablesPedido : DatatablesBase.DataTablesBase<TablaPedidosViewModel,Orden>
    {
        public override List<string[]> generarListaDeArreglo(IEnumerable<TablaPedidosViewModel> model)
        {
            var lista = new List<string[]>();
            foreach (var item in model)
            {
                lista.Add(new string[] {
                    item.idOrden.ToString(),item.cliente,item.ordenFecha,item.ordenHora,item.ordenTotal,item.ordenEstadoPedido,item.ordenTipoPago,item.ordenPagado,item.ordenDetalle
                });
            }
            return lista;
        }
        public override IEnumerable<TablaPedidosViewModel> OrdenaResultados(IEnumerable<TablaPedidosViewModel> model, int idCol, string orden)
        {
            Func<TablaPedidosViewModel, string> funcionOrdenamiento = (c => idCol == 0 ? c.idOrden.ToString() : idCol == 1 ? c.cliente : idCol == 2 ? c.ordenFecha : idCol == 3 ? c.ordenHora : idCol == 4 ? c.ordenTotal : idCol == 5 ? c.ordenTipoPago : c.ordenEstadoPedido);
            if (orden== "asc")
            {
                model= model.OrderBy(funcionOrdenamiento);
            }
            else
            {
                model = model.OrderByDescending(funcionOrdenamiento);
            }
            return model;
        }
        public override IEnumerable<TablaPedidosViewModel> PaginarResultados(int inicio, int toma, IEnumerable<TablaPedidosViewModel> model)
        {
            return model.Skip(inicio).Take(toma);
        }
        public override List<TablaPedidosViewModel> modeloTabla(IQueryable<Orden> dbModel,string busqueda)
        {
            List<TablaPedidosViewModel> pedidosLista = new List<TablaPedidosViewModel>();
            foreach (var item in dbModel)
            {
                pedidosLista.Add(new TablaPedidosViewModel()
                {

                    idOrden = item.idOrden,
                    ordenDetalle = generarBoton(item),
                    ordenEstadoPedido = item.ordenEstadoPedido,
                    ordenFecha = item.ordenFecha.ToShortDateString(),
                    ordenHora = item.ordenFecha.ToShortTimeString(),
                    ordenPagado = item.ordenPagado == true ? "Pagado" : "Pendiente de pago",
                    ordenTipoPago = item.ordenTipoPago,
                    ordenTotal = item.ordenTotal.ToString("#.##"),
                    cliente = item.cliente.nombreCliente
                });
            }
            if (!string.IsNullOrEmpty(busqueda))
            {
                pedidosLista = pedidosLista.Where(a => a.idOrden.ToString().Contains(busqueda)).ToList();
            }
            return pedidosLista;
        }
        private string generarBoton(Orden item)
        {
            var url = string.Format("<a href='{0}'>Detalle</a>","/Administrador/Pedidos/DetallePedido"+item.idOrden);
            return url;
        }

    }
}
