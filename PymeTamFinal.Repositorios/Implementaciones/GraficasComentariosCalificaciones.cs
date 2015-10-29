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
    public class GraficasComentariosCalificaciones : GraficasSegundaVersionBase<GraficaBarras,CajaComentarios>
    {
        public GraficasComentariosCalificaciones(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
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
                                 total = grp.Key
                             };
            List<seriesBarras> series = new List<seriesBarras>();
            List<decimal> data = new List<decimal>();
            foreach (var item in agrupacion)
            {
                data.Add(item.total);
            }
            series.Add(new seriesBarras() {
                data = data.ToArray(),
                name = "Todos los productos"
            });
            grafica.series = series.ToArray();
            grafica.title = new title() {
                text = "Proporción de calificaciones",
                
            };
            grafica.xAxis = new xAxis() {
                categories = categorias
            };
            return grafica;
        }
    }
}
