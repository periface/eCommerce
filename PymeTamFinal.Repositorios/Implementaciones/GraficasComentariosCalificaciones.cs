using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.GraficasBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    public class GraficasComentariosCalificaciones : GraficasSegundaVersionBase<GraficaBarras, CajaComentarios>
    {
        public GraficasComentariosCalificaciones(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override GraficaBarras GenerarGraficaParamPers(object model)
        {
            GraficaCalificacionesCProducto modelo = new GraficaCalificacionesCProducto();

            if (model.GetType() == typeof(GraficaCalificacionesCProducto))
            {
                GraficaBarras grafica = new GraficaBarras();
                string[] categorias = new[] { "5 Estrellas", "4 Estrellas", "3 Estrellas", "2 Estrellas", "1 Estrellas" };
                //PRODUCTO
                modelo = (GraficaCalificacionesCProducto)model;

                var producto = context.Producto.Find(modelo.idProducto);

                var filtradoProducto = modelo.comentarios.Where(a => a.idProducto == producto.idProducto);
                var agrupacionProducto = from p in filtradoProducto
                                         group p by p.calificacion into grpCal
                                         select new
                                         {
                                             cal = grpCal.Key, tot = grpCal.Count()
                                         };
                List<seriesBarras> series = new List<seriesBarras>();
                List<decimal> dataproducto = new List<decimal>();
                foreach (var item in agrupacionProducto)
                {
                    dataproducto.Add(item.tot);
                }
                
                //FIN PRODUCTO
                var agrupacionTodos = from c in modelo.comentarios
                                 group c by c.calificacion into grp
                                 select new
                                 {
                                     calificacion = grp.Key,
                                     total = grp.Count(),
                                 };
                List<decimal> dataTodos = new List<decimal>();
                foreach (var item in agrupacionTodos)
                {
                    dataTodos.Add(item.total);
                }
                series.Add(new seriesBarras()
                {
                    data = dataTodos.ToArray(),
                    name = "Todos los productos"
                });
                series.Add(new seriesBarras()
                {
                    data = dataproducto.ToArray(),
                    name = producto.nombreProducto,
                    type = "spline"
                });
                grafica.series = series.ToArray();
                grafica.title = new title()
                {
                    text = "Proporción de calificaciones",

                };
                grafica.xAxis = new xAxis()
                {
                    categories = categorias
                };
                return grafica;
            }
            return base.GenerarGraficaParamPers(model);
        }
        public override GraficaBarras GenerarGraficaBaseEnumerable(IEnumerable<CajaComentarios> model)
        {
            string[] categorias = new[] { "5 Estrellas", "4 Estrellas", "3 Estrellas", "2 Estrellas", "1 Estrellas" };
            GraficaBarras grafica = new GraficaBarras();
            var agrupacion = from c in model
                             group c by c.calificacion into grp
                             select new
                             {
                                 calificacion = grp.Key,
                                 total = grp.Count()
                             };
            List<seriesBarras> series = new List<seriesBarras>();
            List<decimal> data = new List<decimal>();
            foreach (var item in agrupacion)
            {
                data.Add(item.total);
            }
            series.Add(new seriesBarras()
            {
                data = data.ToArray(),
                name = "Todos los productos"
            });
            grafica.series = series.ToArray();
            grafica.title = new title()
            {
                text = "Proporción de calificaciones",

            };
            grafica.xAxis = new xAxis()
            {
                categories = categorias
            };
            return grafica;
        }
    }
}
