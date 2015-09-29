using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Controles
{
    public class ClienteController : Controller
    {
        public ViewResult error404Tienda
        {
            get
            {
                return View("_error404Tienda");
            }
        }
    }
}
