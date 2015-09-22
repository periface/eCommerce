using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Controles
{
    public class AdminController : Controller
    {
        public string error404Parcial {
            get { return "_error404ParcialModal"; }
        }
        public string error500Parcial
        {
            get { return "_error500Parcial"; }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            if (filterContext.Exception.HResult == 500) {

            }
            filterContext.Result = new ViewResult() {
                ViewName = error500Parcial
            };
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
