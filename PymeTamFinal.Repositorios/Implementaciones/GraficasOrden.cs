using PymeTamFinal.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.GraficasBase;
using System.Linq.Expressions;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    public class GraficasOrden : GraficasBase<Orden>
    {
        public GraficasOrden(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override object generarGrafica(IQueryable<Orden> data)
        {
            var datosConvertidos = data;
            var datosConvertidosEnum = datosConvertidos.AsEnumerable();
            GraficaVolumenVentas grafica = new GraficaVolumenVentas();
            //Se puede reducir en el constructor
            grafica.rangeSelector = new rangeSelector()
            {

                selected = 1
            };
            var listSeries = new List<series>();
            //Solo agregamos una serie al volumen de ventas
            listSeries.Add(new series()
            {
                name = "Volumen General (Concretadas)",
                //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
                //el codigo dentro del mismo no es reconocido por EntityFrameWork
                data = datosConvertidosEnum.Where(a => a.ordenPagado == true).OrderBy(a => a.ordenFecha).Select(x => new[] { EpochMillis(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            listSeries.Add(new series()
            {
                name = "Volumen General (No Concretadas)",
                //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
                //el codigo dentro del mismo no es reconocido por EntityFrameWork
                data = datosConvertidosEnum.Where(a => a.ordenPagado == false).OrderBy(a => a.ordenFecha).Select(x => new[] { EpochMillis(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            //listSeries.Add(new series()
            //{
            //    type = "pie",
            //    name = "Volumen General (No Concretadas)",
            //    //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
            //    //el codigo dentro del mismo no es reconocido por EntityFrameWork
            //    data = datosConvertidosEnum.Where(a => a.ordenPagado == false).OrderBy(a => a.ordenFecha).Select(x => new[] { EpochMillis(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            //});
            grafica.series = listSeries;

            grafica.title = new title()
            {
                text = "Volumen de ventas"
            };
            return grafica;
        }
        public override object generarGraficaAgrupacion(string prop)
        {
            //Las graficas de pastel a diferencia de la grafica de linea tiene una lista de datos
            //La segunda tiene una lista de series
            GraficaPastel pastel = new GraficaPastel();
            var pedidos = context.Orden.AsQueryable();
            var agrupacion = from d in pedidos
                             group pedidos by d.ordenEstadoPedido into grp
                             select new
                             {
                                 grpnombre = grp.Key,
                                 grpCnt = grp.Count()
                             };
            List<seriesPastel> series = new List<seriesPastel>();
            List<dataPastel> data = new List<dataPastel>();
            bool primero = true;
            foreach (var item in agrupacion)
            {
                bool sliced = false;
                if (primero)
                {
                    sliced = true;
                }
                data.Add(new dataPastel()
                {
                    name = item.grpnombre,
                    sliced = sliced,
                    y = item.grpCnt
                });
                primero = false;
            }
            series.Add(new seriesPastel()
            {
                name = "Porcentaje de distribución de estatus",
                data = data,
                colorByPoint = true,
                innerSize = "50%"
            });
            pastel.chart = new chart()
            {
                plotShadow = false,
                type = "pie"
            };
            pastel.title = new title()
            {
                text = "Estatús de pedidos",
            };
            pastel.series = series;

            return pastel;
        }
        public override object generarGraficaAgrupacion()
        {
            //Las graficas de pastel a diferencia de la grafica de linea tiene una lista de datos
            //La segunda tiene una lista de series
            GraficaPastel pastel = new GraficaPastel();
            var pedidos = context.Orden.AsQueryable();
            var agrupacion = from d in pedidos
                             group pedidos by d.ordenTipoPago into grp
                             select new
                             {
                                 grpnombre = grp.Key,
                                 grpCnt = grp.Count()
                             };
            List<seriesPastel> series = new List<seriesPastel>();
            List<dataPastel> data = new List<dataPastel>();
            bool primero = true;
            foreach (var item in agrupacion)
            {
                bool sliced = false;
                if (primero)
                {
                    sliced = true;
                }
                data.Add(new dataPastel()
                {
                    name = item.grpnombre,
                    sliced = sliced,
                    y = item.grpCnt
                });
                primero = false;
            }
            series.Add(new seriesPastel()
            {
                name = "Porcentaje de utilización",
                data = data,
                colorByPoint = true
            });
            pastel.chart = new chart()
            {
                plotShadow = false,
                type = "pie"
            };
            pastel.title = new title()
            {
                text = "Tipos de pago utilizados",
            };
            pastel.series = series;

            //return pastel;
            //Llame un metodo privado que regresa un objeto de otro tipo
            return generarGraficaWDrill();
        }
        private object generarGraficaWDrill()
        {
            var series = new List<seriesPastelWdrill>();
            var dataDrill = new List<dataPastelWdrill>();
            drillDown data = new drillDown();
            GraficaPastelWdrill grafica = new GraficaPastelWdrill();
            //Las graficas de pastel a diferencia de la grafica de linea tiene una lista de datos
            //La segunda tiene una lista de series
            var ordenes = context.Orden.AsEnumerable();
            var agrupacionPorPago = from o in ordenes
                                    group o by o.ordenTipoPago into grp
                                    select new
                                    {
                                        tipo = grp.Key,
                                        cont = grp.Count()
                                    };
            List<string> _categorias = new List<string>();
            foreach (var tipoEncontrado in agrupacionPorPago)
            {
                _categorias.Add(tipoEncontrado.tipo);
                var dataElm = new dataPastelWdrill()
                {
                    y = tipoEncontrado.cont,
                };
                var agrupacionEstadoPago = from o in ordenes
                                           where o.ordenTipoPago == tipoEncontrado.tipo
                                           group o by o.ordenEstadoPedido into grp
                                           select new
                                           {
                                               estado = grp.Key,
                                               cantidad = grp.Count()
                                           };
                drillDown drill = new drillDown();
                List<string> categorias = new List<string>();
                List<double> dataDrll = new List<double>();
                drill.name = tipoEncontrado.tipo;
                foreach (var item in agrupacionEstadoPago)
                {
                    categorias.Add(item.estado);
                    dataDrll.Add(item.cantidad);
                }
                drill.categories = categorias.ToArray();
                drill.data = dataDrll.ToArray();
                dataElm.drillDown = drill;
                dataDrill.Add(dataElm);
                
            }
            series.Add(new seriesPastelWdrill() {
                data = dataDrill
            });
            grafica.series = series;
            grafica.chart = new chart()
            {
                type = "pie",

            };
            grafica.title = new title()
            {
                text = "Asignación de estados",
            };
            grafica.categorias = _categorias.ToArray();
            return grafica;
        }
    }
}
