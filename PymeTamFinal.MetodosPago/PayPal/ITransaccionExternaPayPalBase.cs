﻿using PymeTamFinal.Contratos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.MetodosPago.PayPal
{
    public class ITransaccionExternaPayPalBase<T> : ITransaccionExterna<T> where T :class
    {

        public virtual bool ComprobarConexion(string apiKey, string apiSecret, out string error)
        {
            throw new NotImplementedException();
        }

        public virtual bool EjecutarPago(int idPago, int idOrden, int idComprador)
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
