using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.ClientesHelper
{
    public static class ComentariosHelper
    {
        public static IHtmlString generarEstrellasCalif(this HtmlHelper helper, int calif)
        {
            if (calif == 0) {
                return new HtmlString("<p>Sin calificaciones</p>");
            }
            if (calif <= 0)
            {
                TagBuilder span = new TagBuilder("span");
                span.MergeAttribute("class", "glyphicon glyphicon-star");
                return new HtmlString(span.ToString());
            }
            string elm = string.Empty;
            for (int i = 0; i < calif; i++)
            {
                TagBuilder span = new TagBuilder("span");
                span.MergeAttribute("class", "glyphicon glyphicon-star");

                elm += " " + span.ToString();
            }
            return new HtmlString(elm.ToString());
        }
    }
}
