using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Models
{
    public abstract class IMetodoPago
    {
        public ActionResult result { get; set; }
        public string nombreMetodo { get; set; }
        public ActionResult resultPagoTardio { get; set; }
        public abstract void guardarEstadistica();
    }
}
