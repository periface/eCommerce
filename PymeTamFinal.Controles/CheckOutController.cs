using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Controles
{
    public class CheckOutController : Controller
    {
        public ViewResult paypalTransaccion {
            get {
                return new ViewResult() {
                    ViewName = "Paypal"
                };
            }
        }
        public ViewResult depositoInfo
        {
            get
            {
                return new ViewResult()
                {
                    ViewName = "Paypal"
                };
            }
        }
        public ViewResult otroInfo
        {
            get
            {
                return new ViewResult()
                {
                    ViewName = "Paypal"
                };
            }
        }
    }
}
