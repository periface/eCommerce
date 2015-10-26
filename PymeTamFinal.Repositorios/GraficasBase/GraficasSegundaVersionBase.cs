using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.GraficasBase
{
    /// <summary>
    /// Modelo hace referencia a la tabla de sql
    /// Tipo hace referencia al tipo de grafica que se requiere
    /// Aunque requiere crear implementaciónes para los tipos de grafica
    /// resuelve el problema de agregar metodos a la interface de contratos,
    /// lo cual no es correcto bajo ninguna circunstancia, claro que cualquier idea
    /// es bienvenida (no es cierto.)
    /// </summary>
    /// <typeparam name="Tipo"></typeparam>
    /// <typeparam name="Modelo"></typeparam>
    public class GraficasSegundaVersionBase<Tipo, Modelo> : IGeneradorGraficasVersionNueva<Tipo, Modelo> where Tipo : class where Modelo : class
    {
        #region Props
        internal DataContext context;
        internal DbSet<Modelo> dbSet;
        #endregion
        public GraficasSegundaVersionBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<Modelo>();
        }
        public virtual double FechaAMilliSeg(DateTime fecha)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = fecha - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }

        public virtual Tipo GenerarGrafica(Modelo model)
        {
            throw new NotImplementedException();
        }

        public virtual Tipo GenerarGraficaBaseEnumerable(IEnumerable<Modelo> model)
        {
            throw new NotImplementedException();
        }

        public virtual List<Tipo> GenerarGraficas(Modelo model)
        {
            throw new NotImplementedException();
        }

        public virtual List<Tipo> GenerarGraficasBaseEnumerable(IEnumerable<Modelo> model)
        {
            throw new NotImplementedException();
        }
    }
}
