using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.Stripe
{
    //No usado
    public class ITransaccionExternaStripeBase<T> : ITransaccionExterna<T> where T : class
    {
        //Mi comentario
        #region Props
        internal DataContext context;
        internal DbSet<T> dbSet;
        #endregion
        public ITransaccionExternaStripeBase(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
        public virtual bool ComprobarConexion(string apiKey, string apiSecret, out string error)
        {
            throw new NotImplementedException();
        }

        public virtual bool EjecutarPago(string idPago, int idOrden, string idComprador, string api, string secret)
        {
            throw new NotImplementedException();
        }

        public virtual object GenerarContexto(string api, string secret)
        {
            throw new NotImplementedException();
        }

        public virtual string GenerarToken(T modeloRequerido, string apiKey, string apiSecret)
        {
            throw new NotImplementedException();
        }
    }
}
