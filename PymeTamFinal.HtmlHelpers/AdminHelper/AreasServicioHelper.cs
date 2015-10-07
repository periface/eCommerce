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
    public static class AreasServicioHelper
    {
        //<input type="checkbox" data-envio="@metodo.idEnvio" data-ciudad="@ViewBag.id" /> 
        public static IHtmlString envioEstadoChk(this HtmlHelper helper,int idEnvio,int idEstado) {
            
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type","checkbox");
            input.MergeAttribute("class", "checkEnvio");
            input.MergeAttribute("data-estado",idEstado.ToString());
            input.MergeAttribute("data-envio", idEnvio.ToString());
            IRepositorioBase<CostosEnvio> _envios = new RepositorioEnvios(new CapaDatos.DataContext());
            //Cargamos el envio
            var envio = _envios.CargarPorId(idEnvio);
            //Primer paso, revisar que exista la relación entre los 2 elementos envio y ciudad
            if (envio.estados.Any(a => a.idEstado== idEstado)) {
                //La ciudad ya esta relacionada con este envio
                input.MergeAttribute("checked","checked");
            }
            return new HtmlString(input.ToString(TagRenderMode.SelfClosing));
        }
    }
}
