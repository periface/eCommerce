using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.ClientesHelper
{
    public static class Header
    {
        /// <summary>
        /// Carga de una lista de metatags personalizados desde la base de datos
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string MetaTags(this HtmlHelper helper) {
            return "";
        }
        /// <summary>
        /// Si el tema de bootstrap esta activo, se cargan los archivos css necesarios
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ArchivosCss(this HtmlHelper helper)
        {
            return "";
        }
    }
}
