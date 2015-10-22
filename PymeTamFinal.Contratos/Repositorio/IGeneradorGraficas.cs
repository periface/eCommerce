using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IGeneradorGraficas<T> where T :class
    {
        object generarGrafica(IQueryable<T> data);
        object generarGraficaAgrupacion();
        object generarGraficaAgrupacion(string prop);
        double EpochMillis(DateTime fecha);
    }
}
