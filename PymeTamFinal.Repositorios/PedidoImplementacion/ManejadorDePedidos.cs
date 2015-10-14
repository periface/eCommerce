using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.TransaccionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.PedidoImplementacion
{
    public class ManejadorDePedidos : TransaccionBase<compraModel>
    {
        public ManejadorDePedidos(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }

        public override void guardarOrden(compraModel orden, string cartId, string idUsuario)
        {
            var model = new Orden();
            if (!string.IsNullOrEmpty(orden.cupon)) {

            }
            var carroItems = context.CarritoCompra.Where(a=>a.idCarro==cartId).ToList();
            var usuario = context.Cliente.Where(a=>a.idAsp==idUsuario).SingleOrDefault();
        }
    }
}
