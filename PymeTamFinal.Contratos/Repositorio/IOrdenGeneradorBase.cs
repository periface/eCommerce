using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IOrdenGeneradorBase<T> where T : class
    {
        /// <summary>
        /// Guarda la orden en persistencia
        /// </summary>
        /// <param name="orden"></param>
        void guardarOrden(T orden, string cartId, string idUsuario, decimal descuento, object httpContext);
        int cargaContexto(object context);
        void limpiaContexto(object context);
        object cargaOrden(object id);
        void actualizarMetodoDePago(int? idOrden,string metodo,object context);
        void agregarTicket(int? idOrden, string ruta);
    }
}
