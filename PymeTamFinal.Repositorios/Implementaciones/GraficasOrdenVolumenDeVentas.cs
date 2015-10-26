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
    public class GraficasOrdenVolumenDeVentas : GraficasSegundaVersionBase<IGrafica,Orden>
    {
        public GraficasOrdenVolumenDeVentas(DataContext context) :base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IGrafica GenerarGraficaBaseEnumerable(IEnumerable<Orden> model)
        {
            var datosConvertidosEnum = model;
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
                data = datosConvertidosEnum.Where(a => a.ordenPagado == true).OrderBy(a => a.ordenFecha).Select(x => new[] { FechaAMilliSeg(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            listSeries.Add(new series()
            {
                name = "Volumen General (No Concretadas)",
                //Cuando hablamos de IQueryables estamos enviando consultas a la base de datos, por lo tanto el metodo EpoChMillis nos arroja un error ya que
                //el codigo dentro del mismo no es reconocido por EntityFrameWork
                data = datosConvertidosEnum.Where(a => a.ordenPagado == false).OrderBy(a => a.ordenFecha).Select(x => new[] { FechaAMilliSeg(x.ordenFecha.Date), (double)x.ordenTotal }).ToArray()
            });
            //Posible necesidad de agregar ventas canceladas
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
            //Los siguientes codigos funcionan
            //return new GraficaPastel();
            //Como resolver el problema de la inflexibiliad:
            //Hacer que ob se comporte como GraficaVolumenVentas,
            //Entonces deberiamos crear una clase base y regresar esa clase en lugar del tipo
            //especificado actualmente
            //var ob = new object();
            //return ob as GraficaVolumenVentas;
            //HECHO! pero no probado
        }
        public override List<IGrafica> GenerarGraficasBaseEnumerable(IEnumerable<Orden> model)
        {
            return base.GenerarGraficasBaseEnumerable(model);
        }
    }
}
