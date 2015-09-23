using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Attributos
{
    /// <summary>
    /// Para vistas no autorizadas en modals
    /// </summary>
    public class AdminAutorizacionParcialAttr : AuthorizeAttribute
    {
        string nombreVista;
        private ViewResult errorNoAutorizadoParcial {
            get {
                var viewResult = new ViewResult();
                viewResult.ViewName = string.IsNullOrEmpty(nombreVista) ? "_NoAutorizadoParcialModal":nombreVista;
                return viewResult;
            }
        }
        /// <summary>
        /// Recibe un parametro para enviar una vista personalizada en caso de tenerla
        /// </summary>
        /// <param name="vista"></param>
        public AdminAutorizacionParcialAttr(string vista)
        {
            nombreVista = vista;
        }
        public AdminAutorizacionParcialAttr()
        {

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = errorNoAutorizadoParcial;
        }
    }
    public class AdminAutorizacionAttr :AuthorizeAttribute {
        string nombreVista;
        private ViewResult errorNoAutorizadoParcial
        {
            get
            {
                var viewResult = new ViewResult();
                viewResult.ViewName = string.IsNullOrEmpty(nombreVista) ? "_NoAutorizadoParcialModal" : nombreVista;
                return viewResult;
            }
        }
        /// <summary>
        /// Recibe un parametro para enviar una vista personalizada en caso de tenerla
        /// </summary>
        /// <param name="vista"></param>
        public AdminAutorizacionAttr(string vista)
        {
            nombreVista = vista;
        }
        public AdminAutorizacionAttr()
        {

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = errorNoAutorizadoParcial;
        }
    }
}
