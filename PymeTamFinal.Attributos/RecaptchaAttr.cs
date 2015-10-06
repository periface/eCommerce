using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace PymeTamFinal.Attributos
{
    public class objRecaptcha
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
    /// <summary>
    /// Attributo para validar el recaptcha de google
    /// <para>Esto tiene efecto en la misma accion donde esta el captcha</para>
    /// <para>se necesita resolver el problema cuando las acciones post son llamadas desde _partials</para>
    /// </summary>
    public class RecaptchaAttr : ActionFilterAttribute, IActionFilter
    {
        private string _controlador;
        private string _accion;
        private bool redireccionarAVista;
        public RecaptchaAttr()
        {

        }
        public RecaptchaAttr(string accion, string controlador)
        {
            _controlador = controlador;
            _accion = accion;
            redireccionarAVista = true;
        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!redireccionarAVista)
            {
                redireccionarNormal(filterContext);
            }
            else
            {
                if (filterContext.RequestContext.HttpContext.Request["idElemento"] != null && filterContext.RequestContext.HttpContext.Request["slug"] != null)
                {
                    //Para caja de comentarios
                    //Es la solución provicional para la caja de comentarios de los productos,
                    //que es llamada mediante un partialView
                    var id = filterContext.RequestContext.HttpContext.Request["idElemento"];
                    var slug = filterContext.RequestContext.HttpContext.Request["slug"];
                    redireccionarGenerico(_controlador, _accion, id, slug, filterContext);
                }
                else
                {
                    redireccionarGenerico(_controlador, _accion, filterContext);
                }
            }
        }

        private void redireccionarGenerico(string _controlador, string _accion, ActionExecutingContext filterContext)
        {
            var respuesta = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
            const string secret = "6Lco7g0TAAAAAHT5ReCjd-zTtok7J_slEvRI3XiO";
            var cliente = new WebClient();
            var peticion = cliente.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, respuesta));
            var captchaRepuesta = JsonConvert.DeserializeObject<objRecaptcha>(peticion);
            if (captchaRepuesta.Success)
            {
                //Ejecutamos normal
                OnActionExecuting(filterContext);
            }
            else
            {
                if (captchaRepuesta.ErrorCodes.Count <= 0)
                {
                    OnActionExecuting(filterContext);
                }
                var error = captchaRepuesta.ErrorCodes[0].ToLower();
                var result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", _controlador},
                    {"action", _accion},
                });
                switch (error)
                {
                    case ("missing-input-secret"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";

                        filterContext.Result = result;
                        break;
                    case ("invalid-input-secret"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;

                    case ("missing-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;
                    case ("invalid-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;

                    default:
                        filterContext.Controller.ViewBag.Message = "Ocurrio un error, intentelo de nuevo";
                        filterContext.Result = result;
                        break;
                }

            }
        }
        private void redireccionarGenerico(string _controlador, string _accion, string id, string slug, ActionExecutingContext filterContext)
        {
            var respuesta = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
            const string secret = "6Lco7g0TAAAAAHT5ReCjd-zTtok7J_slEvRI3XiO";
            var cliente = new WebClient();
            var peticion = cliente.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, respuesta));
            var captchaRepuesta = JsonConvert.DeserializeObject<objRecaptcha>(peticion);
            if (captchaRepuesta.Success)
            {
                //Ejecutamos normal
                OnActionExecuting(filterContext);
            }
            else
            {
                if (captchaRepuesta.ErrorCodes.Count <= 0)
                {
                    OnActionExecuting(filterContext);
                }
                var error = captchaRepuesta.ErrorCodes[0].ToLower();
                var result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", _controlador},
                    {"action", _accion},
                    { "id",id },
                    { "slug",slug }
                });
                switch (error)
                {
                    case ("missing-input-secret"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";

                        filterContext.Result = result;
                        break;
                    case ("invalid-input-secret"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;

                    case ("missing-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;
                    case ("invalid-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        filterContext.Result = result;
                        break;

                    default:
                        filterContext.Controller.ViewBag.Message = "Ocurrio un error, intentelo de nuevo";
                        filterContext.Result = result;
                        break;
                }

            }
        }
        private void redireccionarNormal(ActionExecutingContext filterContext)
        {
            var respuesta = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
            const string secret = "6Lco7g0TAAAAAHT5ReCjd-zTtok7J_slEvRI3XiO";
            var cliente = new WebClient();
            var peticion = cliente.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, respuesta));
            var captchaRepuesta = JsonConvert.DeserializeObject<objRecaptcha>(peticion);
            if (captchaRepuesta.Success)
            {
                //Ejecutamos normal
                OnActionExecuting(filterContext);
            }
            else
            {
                if (captchaRepuesta.ErrorCodes.Count <= 0)
                {
                    OnActionExecuting(filterContext);
                }
                var error = captchaRepuesta.ErrorCodes[0].ToLower();

                switch (error)
                {
                    case ("missing-input-secret"):
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult()
                            {
                                Data = new { mensaje = "Respuesta no valida" },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            filterContext.Controller.ViewBag.Message = "Respuesta no valida.";

                            filterContext.Result = new ViewResult()
                            {
                                ViewName = filterContext.ActionDescriptor.ActionName,
                                TempData = filterContext.Controller.TempData,
                                ViewData = filterContext.Controller.ViewData,

                            };
                        }
                        break;
                    case ("invalid-input-secret"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult()
                            {
                                Data = new { mensaje = "Respuesta no valida" },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            filterContext.Result = new ViewResult()
                            {
                                ViewName = filterContext.ActionDescriptor.ActionName,
                                TempData = filterContext.Controller.TempData,
                                ViewData = filterContext.Controller.ViewData,

                            };
                        }

                        break;

                    case ("missing-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult()
                            {
                                Data = new { mensaje = "Respuesta no valida" },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            filterContext.Result = new ViewResult()
                            {
                                ViewName = filterContext.ActionDescriptor.ActionName,
                                TempData = filterContext.Controller.TempData,
                                ViewData = filterContext.Controller.ViewData,

                            };
                        }

                        break;
                    case ("invalid-input-response"):
                        filterContext.Controller.ViewBag.Message = "Respuesta no valida.";
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult()
                            {
                                Data = new { mensaje = "Respuesta no valida" },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            filterContext.Result = new ViewResult()
                            {
                                ViewName = filterContext.ActionDescriptor.ActionName,
                                TempData = filterContext.Controller.TempData,
                                ViewData = filterContext.Controller.ViewData,

                            };
                        }

                        break;

                    default:
                        filterContext.Controller.ViewBag.Message = "Ocurrio un error, intentelo de nuevo";
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult()
                            {
                                Data = new { mensaje = "Respuesta no valida" },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            filterContext.Result = new ViewResult()
                            {
                                ViewName = filterContext.ActionDescriptor.ActionName,
                                TempData = filterContext.Controller.TempData,
                                ViewData = filterContext.Controller.ViewData,

                            };
                        }

                        break;
                }

            }
        }
    }
}
