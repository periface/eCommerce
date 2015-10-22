using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Models
{
    public class metodoPagoDeposito : IMetodoPago
    {
        private int Orden;
        public metodoPagoDeposito(string rtname,int orden)
        {
            result = new RedirectResult(rtname);
            Orden = orden;
        }
        public metodoPagoDeposito(string rtname)
        {
            result = new RedirectResult(rtname);
        }
        public override void guardarEstadistica()
        {
            throw new NotImplementedException();
        }
    }
}
