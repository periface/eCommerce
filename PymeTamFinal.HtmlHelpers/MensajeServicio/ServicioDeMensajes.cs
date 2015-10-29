using PymeTamFinal.HtmlHelpers.Abstraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.MensajeServicio
{
    /// <summary>
    /// Es posible mejorar el servicio utilizando una clase no estatica
    /// que durante su constructor envie el ControllerContext, esto para evitar enviar
    /// el mismo cada vez que la clase estatica lo requiera
    /// </summary>
    public static class ServicioDeMensajes
    {
        public static void obtieneMensaje(ControllerBase controller) {
            var mensaje = controller.TempData["mensaje"];
            if (mensaje == null)
                return;
            var estado = (enumMensaje)mensaje;
            switch (estado)
            {
                case enumMensaje.Agregado:
                    controller.ViewBag.mensajeSituacion = "Elemento agregado correctamente";
                    controller.ViewBag.tipoMensaje = "#3276B1";
                    break;
                case enumMensaje.Editado:
                    controller.ViewBag.mensajeSituacion = "Elemento editado correctamente";
                    controller.ViewBag.tipoMensaje = "#3276B1";
                    break;
                case enumMensaje.Eliminado:
                    controller.ViewBag.mensajeSituacion = "Elemento eliminado correctamente";
                    controller.ViewBag.tipoMensaje = "#C46A69";
                    break;
                case enumMensaje.ErrorRecurrencia:
                    controller.ViewBag.mensajeSituacion = "El elemento no puede ser hijo del mismo elemento.";
                    controller.ViewBag.tipoMensaje = "#C46A69";
                    break;
                case enumMensaje.Habilitado:
                    controller.ViewBag.mensajeSituacion = "El elemento se ha habilitado correctamente.";
                    controller.ViewBag.tipoMensaje = "#3276B1";
                    break;
                case enumMensaje.ErrorBasico:
                    controller.ViewBag.mensajeSituacion = "Ocurrio un error durante la operación.";
                    controller.ViewBag.tipoMensaje = "#C46A69";
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
    public static class ServicioDeMensajesCliente {
        
        public static void obtieneMensaje(ControllerBase controller) {

        }
        public static void darMensaje(enumMensajeCliente mensaje, ControllerBase controller) {

        }
    }
    /// <summary>
    /// Servicio no estatico, requiere instanciacion y el controllerContext
    /// </summary>
    public class ServicioMensajes : IServicioMensajes {
     
        private ControllerBase controller;
        public ServicioMensajes(ControllerBase controller)
        {
            this.controller = controller;
        }
        public enum enumMensaje
        {
            Agregado,
            Editado,
            Eliminado,
            ErrorRecurrencia,
            Habilitado,
            ErrorBasico
        }
        public void obtieneMensaje()
        {
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
                case enumMensaje.ErrorBasico:
                    controller.ViewBag.mensajeSituacion = "Ocurrio un error durante la operación.";
                    controller.ViewBag.tipoMensaje = "error";
                    break;
                default:
                    break;
            }
            controller.TempData.Clear();
        }
        public void darMensaje(enumMensaje mensaje)
        {
            controller.TempData["mensaje"] = mensaje;
        }

        public void obtieneMensaje(ControllerBase controller)
        {
            throw new NotImplementedException();
        }

        public void darMensaje(Abstraccion.enumMensaje mensaje, ControllerBase controller)
        {
            throw new NotImplementedException();
        }
    }
}
