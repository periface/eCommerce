using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.AdminHelper
{
    public static class EmpresaHelpers
    {
        public static string ClasePanelEmpresa(this HtmlHelper helper,bool empresaActiva) {
            if (empresaActiva)
            {
                return "panel panel-success";
            }
            else {
                return "panel panel-danger";
            }
        }
        public static IHtmlString BotonPanelEmpresa(this HtmlHelper helper, Empresa empresa) {
            var Url = new UrlHelper(helper.ViewContext.RequestContext);
            if (empresa.infoActiva)
            {
                return new HtmlString("");
            }
            else {
                TagBuilder a = new TagBuilder("a");
                Dictionary<string, string> attr = new Dictionary<string, string>();
                attr.Add("class", "pull-right btn btn-primary btn-sm");
                attr.Add("href",Url.Action("ActivarInfo","Empresa", new { id=empresa.idEmpresa }));
                a.MergeAttributes(attr);
                a.SetInnerText("Activar esta informacion");
                return new HtmlString(a.ToString());
            }
        }
    }
}
