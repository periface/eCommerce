using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PymeTamFinal.Repositorios.GraficasBase
{
    public class GraficasBase<T> : IGeneradorGraficas<T> where T :class
    {
        #region Props
        internal DataContext context;
        internal DbSet<T> dbSet;
        #endregion
        public GraficasBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        /// <summary>
        /// La implementación base regresa la fecha en milisegundos
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public virtual double EpochMillis(DateTime fecha)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = fecha - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }

        /// <summary>
        /// Base no esta implementada
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual object generarGrafica(IQueryable<T> data)
        {
            throw new NotImplementedException();
        }

        public virtual object generarGraficaAgrupacion(string prop)
        {
            throw new NotImplementedException();
        }

        public virtual object generarGraficaAgrupacion()
        {
            throw new NotImplementedException();
        }
    }
}
