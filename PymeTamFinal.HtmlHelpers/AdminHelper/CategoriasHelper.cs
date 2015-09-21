using PymeTamFinal.CapaDatos;
using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.AdminHelper
{
    public static class CategoriasHelper
    {
        /// <summary>
        /// Revisa si el padre de la categoria aun esta disponible en la base de datos,
        /// 
        /// <para>De ser asi, se muestra el nombre del padre de lo contrario se muestra el mensaje "no categorizada"</para>
        /// <para>De ser root se muestra el mensaje root o categoria raíz</para>
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="idPadre"></param>
        /// <returns></returns>
        public static IHtmlString checaCategorizacion(this HtmlHelper helper, int idPadre)
        {
            IRepositorioBase<Categoria> _categoria = new RepositorioCategorias(new DataContext());
            var categoriaPadre = _categoria.CargarPorId(idPadre);
            TagBuilder span = new TagBuilder("span");
            if (categoriaPadre != null)
            {
                span.MergeAttribute("class", "label label-success");
                span.SetInnerText(categoriaPadre.nombreCategoria);
            }
            else
            {
                if (idPadre == 0)
                {
                    span.MergeAttribute("class", "label label-info");
                    span.SetInnerText("Padre");
                }
                else
                {
                    span.MergeAttribute("class", "label label-danger");
                    span.SetInnerText("Categoria padre no encontrada");
                }

            }
            return new HtmlString(span.ToString());
        }
    }
}
