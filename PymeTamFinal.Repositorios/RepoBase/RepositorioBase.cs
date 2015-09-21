using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using PymeTamFinal.CapaDatos;
using System.Data.Entity;

namespace PymeTamFinal.Repositorios.RepoBase
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        #region Props
        internal DataContext context;
        internal DbSet<T> dbSet;
        #endregion
        public RepositorioBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        #region Sync
        public virtual void Agregar(T entidad)
        {
            throw new NotImplementedException();
        }

        public virtual void Agregar(T entidad, object opt0)
        {
            throw new NotImplementedException();
        }

        public virtual void Agregar(T entidad, object opt0, object opt1)
        {
            throw new NotImplementedException();
        }

        public virtual T AsignarA(object src, object dest)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Cargar()
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Cargar(Expression<Func<T, bool>> lambda)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Cargar(object filtroPersonal)
        {
            throw new NotImplementedException();
        }

        public virtual T CargarPorId(object id)
        {
            throw new NotImplementedException();
        }

        public virtual void Editar(T entidad)
        {
            throw new NotImplementedException();
        }

        public virtual void Editar(T entidad, object opt0)
        {
            throw new NotImplementedException();
        }

        public virtual void Editar(T entidad, object opt0, object opt1)
        {
            throw new NotImplementedException();
        }

        public virtual void Eliminar(T entidad)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Async

        public virtual Task AgregarAsync(T entidad)
        {
            throw new NotImplementedException();
        }

        public virtual Task AgregarAsync(T entidad, object opt0)
        {
            throw new NotImplementedException();
        }

        public virtual Task AgregarAsync(T entidad, object opt0, object opt1)
        {
            throw new NotImplementedException();
        }

        public virtual Task EditarAsync(T entidad)
        {
            throw new NotImplementedException();
        }

        public virtual Task EditarAsync(T entidad, object opt0)
        {
            throw new NotImplementedException();
        }

        public virtual Task EditarAsync(T entidad, object opt0, object opt1)
        {
            throw new NotImplementedException();
        }

        public virtual Task EliminarAsync(T entidad)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> CargarAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> CargarAsync(object filtroPersonal)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> CargarAsync(Expression<Func<T, bool>> lambda)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> CargarPorIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> AsignarAAsync(object src, object dest)
        {
            throw new NotImplementedException();
        }

        public void DesAsociar(T entidad)
        {
            context.Entry(entidad).State = EntityState.Detached;
        }

        public virtual T CargarPorId(object id, string[] tablasOp)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
