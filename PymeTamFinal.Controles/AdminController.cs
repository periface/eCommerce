using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace PymeTamFinal.Controles
{
    public class AdminController : Controller
    {
        /// <summary>
        /// Modals
        /// </summary>
        public ViewResult error500Parcial
        {
            get {
                return View("_error500Parcial");
            }
        }
        public ViewResult error500
        {
            get
            {
                return View("_error500Parcial");
            }
        }
        /// <summary>
        /// Modals
        /// </summary>
        public ViewResult error404Parcial
        {
            get
            {
                return View("_error404ParcialModal");
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;
            if (HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = error500Parcial;
            }
            else {
                filterContext.Result = error500;
            }
            base.OnException(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override void Execute(RequestContext requestContext)
        {
            base.Execute(requestContext);
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}
