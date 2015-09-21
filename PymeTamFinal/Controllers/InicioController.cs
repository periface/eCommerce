using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio-LandingPage
        public async Task<ActionResult> Index()
        {
            EmailService em = new EmailService();
            //await em.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage());
            return View();
        }
    }
}