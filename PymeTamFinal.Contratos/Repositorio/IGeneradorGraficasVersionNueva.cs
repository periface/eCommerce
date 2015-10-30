using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    /// <summary>
    /// Con esta interface se prentende crear graficas de diferentes tipos
    /// con diferentes modelos
    /// </summary>
    public interface IGeneradorGraficasVersionNueva<Tipo,Modelo> where Tipo : class where Modelo : class
    {
        Tipo GenerarGrafica(Modelo model);
        Tipo GenerarGraficaBaseEnumerable(IEnumerable<Modelo> model);
        List<Tipo> GenerarGraficas(Modelo model);
        List<Tipo> GenerarGraficasBaseEnumerable(IEnumerable<Modelo> model);
        double FechaAMilliSeg(DateTime fecha);
        Tipo GenerarGraficaParamPers(object model);
    }
}
