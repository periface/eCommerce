using PymeTamFinal.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    public class GraficaVolumenVentas : GraficasBase.GraficasBase<Modelos.ModelosAuxiliares.GraficaVolumenVentas>
    {
        public GraficaVolumenVentas(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override Modelos.ModelosAuxiliares.GraficaVolumenVentas generarGrafica(object data)
        {
            var datosConvertidos = (IQueryable<Orden>)data;
            var datosConvertidosEnum = datosConvertidos.AsEnumerable();
            Modelos.ModelosAuxiliares.GraficaVolumenVentas grafica = new Modelos.ModelosAuxiliares.GraficaVolumenVentas();
            //Se puede reducir en el constructor
            grafica.rangeSelector = new rangeSelector()
            {

                selected = 1
            };
            var listSeries = new List<series>();
            //Solo agregamos una serie al volumen de ventas
            listSeries.Add(new series() {
                name = "Volumen General (Concretadas)",
                //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
                //el codigo dentro del mismo no es reconocido por EntityFrameWork
                data = datosConvertidosEnum.Where(a=>a.ordenPagado==true).OrderBy(a => a.ordenFecha).Select(x => new[] { EpochMillis(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            //Ejemplo de otra serie
            listSeries.Add(new series()
            {
                name = "Volumen General (No Concretadas)",
                //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
                //el codigo dentro del mismo no es reconocido por EntityFrameWork
                data = datosConvertidosEnum.Where(a=>a.ordenPagado==false).OrderBy(a => a.ordenFecha).Select(x => new[] { EpochMillis(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            grafica.series = listSeries;

            grafica.title = new title()
            {
                text = "Volumen de ventas"
            };
            return grafica;
        }
    }
}
