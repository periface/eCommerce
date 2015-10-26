using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    /// <summary>
    /// Desventaja: Crear nuevos metodos para nuevos tipos de grafica
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGeneradorGraficas<T> where T :class
    {
        object generarGrafica(IQueryable<T> data);
        object generarGraficaAgrupacion();
        object generarGraficaAgrupacion(string prop);
        double EpochMillis(DateTime fecha);
        //Nuevo
        //Nuevo 2
        //Nuevo 3
    }
}
