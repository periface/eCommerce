using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.HtmlHelpers.Abstraccion
{
    public enum enumMensaje
    {
        Agregado,
        Editado,
        Eliminado,
        ErrorRecurrencia,
        Habilitado,
        ErrorBasico,
        ErrorValidacion,
    }
    
    public enum enumMensajeCliente
    {
        NoInformacionUsuarioComentarios,
        NoInformacionUsuarioCarrito,
    }
    public interface IServicioMensajes
    {
        void obtieneMensaje(ControllerBase controller);
        void darMensaje(enumMensaje mensaje, ControllerBase controller);
    }
}
