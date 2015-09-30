using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.BasicHelper
{
    public static class Basic
    {
        public static IHtmlString imagen(this HtmlHelper helper, string imagen, string[] clasesAdicionales)
        {
            TagBuilder img = new TagBuilder("img");
            Dictionary<string, string> attr = new Dictionary<string, string>();
            attr.Add("src", imagen);
            string clases = "img-responsive";
            if (clasesAdicionales != null)
            {
                foreach (var item in clasesAdicionales)
                {
                    clases += " " + item;
                }
            }

            attr.Add("class", clases);

            if (!string.IsNullOrEmpty(imagen))
            {
                img.MergeAttributes(attr);
            }
            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }
        public static string Disponibilidad(this HtmlHelper helper, int stock) {
            if (stock > 0)
            {
                return "En stock ("+ stock+")";
            }
            else {
                return "Agotado";
            }
        }
        public static IHtmlString imagen(this HtmlHelper helper, string imagen, string[] clasesAdicionales, int w, int h)
        {
            TagBuilder img = new TagBuilder("img");
            Dictionary<string, string> attr = new Dictionary<string, string>();
            attr.Add("src", imagen);
            string clases = "img-responsive";
            if (clasesAdicionales != null)
            {
                foreach (var item in clasesAdicionales)
                {
                    clases += " " + item;
                }
            }
            attr.Add("class", clases);
            attr.Add("style", "width:" + w + "px;" + "height:" + h + "px;");
            if (!string.IsNullOrEmpty(imagen))
            {
                img.MergeAttributes(attr);
            }
            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString imagen(this HtmlHelper helper, string imagen, string[] clasesAdicionales, int w, int h, bool usarresizer)
        {
            TagBuilder img = new TagBuilder("img");
            Dictionary<string, string> attr = new Dictionary<string, string>();
            if (usarresizer)
            {
                attr.Add("src", imagen + "?w=" + w + "&h=" + h);
            }
            else
            {
                attr.Add("style", "width:" + w + "px;" + "height:" + h + "px;");
            }
            string clases = "img-responsive";
            if (clasesAdicionales != null)
            {
                foreach (var item in clasesAdicionales)
                {
                    clases += " " + item;
                }
            }
            attr.Add("class", clases);

            if (!string.IsNullOrEmpty(imagen))
            {
                img.MergeAttributes(attr);
            }
            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString imagen(this HtmlHelper helper, string imagen, string[] clasesAdicionales, string w, string h)
        {
            TagBuilder img = new TagBuilder("img");
            Dictionary<string, string> attr = new Dictionary<string, string>();
            attr.Add("src", imagen);
            string clases = "img-responsive";
            if (clasesAdicionales != null)
            {
                foreach (var item in clasesAdicionales)
                {
                    clases += " " + item;
                }
            }
            attr.Add("class", clases);
            attr.Add("style", "width:" + w + ";" + "height:" + h + ";");
            if (!string.IsNullOrEmpty(imagen))
            {
                img.MergeAttributes(attr);
            }
            return new HtmlString(img.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString estado(this HtmlHelper helper, bool estado)
        {
            TagBuilder span = new TagBuilder("span");
            if (estado)
            {
                span.MergeAttribute("class", "label label-success");
                span.SetInnerText("Habilitado(a)");
            }
            else
            {
                span.MergeAttribute("class", "label label-danger");
                span.SetInnerText("Deshabilitado(a)");
            }
            return new HtmlString(span.ToString());
        }
        public static string formatoPrecio(this HtmlHelper helper, decimal precio)
        {
            return "$ " + precio.ToString("#.##") + " MXN";
        }
        public static string displayEstilo(this HtmlHelper helper, Precios model)
        {
            if (model != null)
            {
                if (model.descuentoActivo)
                {
                    return "";
                }
                else
                {

                    return "style=display:none;";
                }
            }
            return "style=display:none;";

        }
        public static string CargaNombre(this HtmlHelper helper, string nombre, string viewBagVal) {
            if (string.IsNullOrEmpty(viewBagVal))
            {
                return nombre;
            }
            else {
                return string.Format("{0} {1}",nombre,viewBagVal);
            }
        }
    }
}
