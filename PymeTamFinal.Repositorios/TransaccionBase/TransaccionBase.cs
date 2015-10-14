using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.TransaccionBase
{
    public class TransaccionBase<T> : ITransaccionBase<T> where T : class
    {
        #region Props
        internal DataContext context;
        internal DbSet<T> dbSet;
        #endregion
        public TransaccionBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public virtual void guardarOrden(T orden, string cartId, string idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
