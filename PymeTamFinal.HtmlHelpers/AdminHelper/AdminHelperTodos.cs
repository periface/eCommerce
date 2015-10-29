using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PymeTamFinal.HtmlHelpers.AdminHelper
{
    public static class AdminHelperTodos
    {
        public static IHtmlString TablaPModel<T>(this HtmlHelper helper, List<T> model,string idProp,string[] urls, params string[] columnas)
        {
            var tipo = model.First().GetType();
            var props = tipo.GetProperties();
            if (props.Count() != columnas.Count())
            {
                throw new InvalidOperationException("La cantidad propiedades el modelo debe ser igual a las columnas");
            }
            TagBuilder tabla = new TagBuilder("table");
            Dictionary<string, string> attrs = new Dictionary<string, string>();
            attrs.Add("class", "table table-responsive table-condensed");
            TagBuilder thead = new TagBuilder("thead");
            StringBuilder theadRows = new StringBuilder();
            theadRows.Append("<tr>");
            //foreach (var item in columnas)
            //{
            //    theadRows.Append("<th>");
            //    theadRows.Append(item.);
            //    theadRows.Append("</th>");
            //}
            theadRows.Append("</tr>");
            TagBuilder tbody = new TagBuilder("tbody");
            StringBuilder tbodyRows = new StringBuilder();
            
            foreach (var item in model)
            {
                tbodyRows.Append("<tr>");
                string val = string.Empty;
                for (int i = 0; i < props.Count(); i++)
                {
                    
                    var valor = props[i].GetValue(item, null);

                    if (valor.GetType() == typeof(DateTime))
                    {
                        var convert = (DateTime)valor;

                        val += (@"<td>" + convert.ToShortDateString() + "</td>");
                    }
                    else {
                        val += (@"<td>" + valor + "</td>");
                    }
                }
                if (urls != null && !string.IsNullOrEmpty(idProp))
                {
                    var id = props.SingleOrDefault(a => a.Name == idProp);
                    if (id != null)
                    {
                        for (int i = 0; i < urls.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    val += (@"<td><a href =" + urls[i] + " >Editar</a><td>");
                                    break;
                                case 1:
                                    val += (@"<td><a href ='" + urls[i] + "/" + id.GetValue(item, null) + "' >Eliminar</a><td>");
                                    break;
                                default:
                                    val += (@"<td><a href ='" + urls[i] + "/" + id.GetValue(item, null) + "' >Opción</a><td>");
                                    break;
                            }
                        }
                    }
                }
                tbodyRows.Append(val);
                
                tbodyRows.Append("</tr>");
            }
            tabla.InnerHtml += theadRows;
            tabla.InnerHtml += tbodyRows;
            tabla.MergeAttributes(attrs);
            return new HtmlString(tabla.ToString());
        }
        public static T cargaAttributo<T>(this MemberInfo member, bool requerido)
            where T : Attribute
        {
            var attr = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();
            if (attr == null && requerido)
            {
                throw new ArgumentException();
            }
            return (T)attr;
        }
        public static string obtenerNombre<T>(Expression<Func<T, object>> expresionProp)
        {
            var memberInfo = cargaInfoDePropiedad(expresionProp.Body);
            if (memberInfo == null)
                throw new ArgumentException();
            var attr = memberInfo.GetCustomAttribute<DisplayNameAttribute>(false);
            if (attr == null)
            {
                return memberInfo.Name;
            }
            else
            {
                return attr.DisplayName;
            }
        }

        private static MemberInfo cargaInfoDePropiedad(Expression body)
        {
            Debug.Assert(body != null, "propertyExpression != null");
            MemberExpression memberExpr = body as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = body as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }
            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }
            return null;
        }
        public static HtmlString ValidationBootstrap(this HtmlHelper htmlHelper, string alertType = "danger",
            string heading = "")
        {
            if (htmlHelper.ViewData.ModelState.IsValid)
                return new HtmlString(string.Empty);

            var sb = new StringBuilder();

            sb.AppendFormat("<div class=\"alert alert-{0} alert-block\">", alertType);
            sb.Append("<button class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");

            if (!string.IsNullOrWhiteSpace(heading))
            {
                sb.AppendFormat("<h4 class=\"alert-heading\">{0}</h4>", heading);
            }

            sb.Append(htmlHelper.ValidationSummary());
            sb.Append("</div>");

            return new HtmlString(sb.ToString());
        }
    }
}
