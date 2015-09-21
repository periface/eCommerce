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
    public static class ProductosHelper
    {
        public static IHtmlString mostrarCategoria(this HtmlHelper helper,Categoria categoria) {
            TagBuilder span = new TagBuilder("span");
            if (categoria!=null)
            {
                span.MergeAttribute("class", "label label-success");
                span.SetInnerText(categoria.nombreCategoria);
            }
            else
            {
                span.MergeAttribute("class", "label label-danger");
                span.SetInnerText("No categorizado");
            }
            return new HtmlString(span.ToString());
        }
    }
}
