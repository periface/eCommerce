using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut
{
    public class CheckOutAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CheckOut";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CheckOut_default",
                "CheckOut/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}