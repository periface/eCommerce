using PymeTamFinal.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class InicioController : ClienteController
    {
        // GET: Inicio-LandingPage
        public ActionResult Index()
        {
            EmailService em = new EmailService();
            //await em.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage());
            return View();
        }
    }
}