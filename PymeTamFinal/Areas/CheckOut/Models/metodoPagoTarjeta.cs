using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Models
{
    public class metodoPagoTarjeta : IMetodoPago
    {
        int Orden;
        public metodoPagoTarjeta(string url,int orden)
        {
            result = new RedirectResult(url);
            Orden = orden;
        }
        public metodoPagoTarjeta(string url)
        {
            result = new RedirectResult(url);
        }
        public override void guardarEstadistica()
        {
            throw new NotImplementedException();
        }
    }
}