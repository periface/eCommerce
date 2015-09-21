using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.MensajeServicio
{
    public static class ServicioDeMensajes
    {
        public enum enumMensaje
        {
            Agregado,
            Editado,
            Eliminado,
            ErrorRecurrencia,
            Habilitado
        }
        public static void obtieneMensaje(ControllerBase controller) {
            var mensaje = controller.TempData["mensaje"];
            if (mensaje == null)
                return;
            var estado = (enumMensaje)mensaje;
            switch (estado)
            {
                case enumMensaje.Agregado:
                    controller.ViewBag.mensajeSituacion = "Elemento agregado correctamente";
                    controller.ViewBag.tipoMensaje = "success";
                    break;
                case enumMensaje.Editado:
                    controller.ViewBag.mensajeSituacion = "Elemento editado correctamente";
                    controller.ViewBag.tipoMensaje = "info";
                    break;
                case enumMensaje.Eliminado:
                    controller.ViewBag.mensajeSituacion = "Elemento eliminado correctamente";
                    controller.ViewBag.tipoMensaje = "warning";
                    break;
                case enumMensaje.ErrorRecurrencia:
                    controller.ViewBag.mensajeSituacion = "El elemento no puede ser hijo del mismo elemento.";
                    controller.ViewBag.tipoMensaje = "error";
                    break;
                case enumMensaje.Habilitado:
                    controller.ViewBag.mensajeSituacion = "El elemento se ha habilitado correctamente.";
                    controller.ViewBag.tipoMensaje = "success";
                    break;
                default:
                    break;
            }
            controller.TempData.Clear();
        }
        public static void darMensaje(enumMensaje mensaje, ControllerBase controller) {
            controller.TempData["mensaje"] = mensaje;
        }
    }
}
