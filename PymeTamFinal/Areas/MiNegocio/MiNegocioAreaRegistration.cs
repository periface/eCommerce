using System.Web.Mvc;

namespace PymeTamFinal.Areas.MiNegocio
{
    public class MiNegocioAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MiNegocio";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MiNegocio_default",
                "MiNegocio/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}