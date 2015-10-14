using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.PayPalEncryptBase
{
    public class PaypalEncryptBase<T> : IPaypalCryptBase<T> where T : class
    {
        #region Props
        internal DataContext context;
        internal DbSet<T> dbSet;
        #endregion
        public PaypalEncryptBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public virtual string Desencriptar(string valor, bool usarHash)
        {
            throw new NotImplementedException();
        }

        public virtual string Encriptar(string valor, bool usarHash)
        {
            throw new NotImplementedException();
        }

        public virtual void Guardar(T model)
        {
            throw new NotImplementedException();
        }
    }
}
