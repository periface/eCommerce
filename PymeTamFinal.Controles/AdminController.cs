using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using PymeTamFinal.Modelos.ModelosVista;

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
        /// Carga los errores del modelo y para enviarlos como json
        /// </summary>
        /// <param name="ModelState"></param>
        /// <param name="ejecucionCorrecta"></param>
        /// <returns></returns>
        public ModeloJson jsonResult(ModelStateDictionary ModelState,bool? ejecucionCorrecta,object modeloEval) {
            var modeloError = new ModeloJson();
            modeloError.ok = ejecucionCorrecta.HasValue? ejecucionCorrecta.Value : false;
            if (!modeloError.ok)
            {
                List<string> model = new List<string>();
                foreach (var resultadoError in ModelState.Values)
                {
                    foreach (var key in resultadoError.Errors)
                    {
                        model.Add(key.ErrorMessage);
                    }
                }
                modeloError.retorno = model;
            }
            else {
                modeloError.retorno = modeloEval;
            }
            return modeloError;
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
            //Log de errores en el modulo de administración
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
            //A partir de aqui se tiene acceso al controllercontext
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //A partir de aqui se tiene acceso al controllercontext
        }
    }
}
