using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IRepositorioBase<T> where T:class
    {
        #region Sync

        void Agregar(T entidad);
        void Agregar(T entidad, object opt0);
        void Agregar(T entidad, object opt0, object opt1);
        void Editar(T entidad);
        void Editar(T entidad, object opt0);
        void Editar(T entidad, object opt0, object opt1);
        void Eliminar(T entidad);
        IQueryable<T> Cargar();
        IQueryable<T> Cargar(object filtroPersonal);
        IQueryable<T> Cargar(Expression<Func<T, bool>> lambda);
        T CargarPorId(object id);
        T CargarPorId(object id, string[] tablasOp);
        T AsignarA(object src, object dest);
        void DesAsociar(T entidad);
        void AgregarRelacion(int entidad0, int entidad1);
        void EliminarRelacion(int entidad0, int entidad1);
        #endregion
        #region Async
        Task AgregarAsync(T entidad);
        Task AgregarAsync(T entidad, object opt0);
        Task AgregarAsync(T entidad, object opt0, object opt1);
        Task EditarAsync(T entidad);
        Task EditarAsync(T entidad, object opt0);
        Task EditarAsync(T entidad, object opt0, object opt1);
        Task EliminarAsync(T entidad);
        Task<IQueryable<T>> CargarAsync();
        Task<IQueryable<T>> CargarAsync(object filtroPersonal);
        Task<IQueryable<T>> CargarAsync(Expression<Func<T, bool>> lambda);
        Task<T> CargarPorIdAsync(object id);
        Task<T> AsignarAAsync(object src, object dest);
        #endregion
    }
}
