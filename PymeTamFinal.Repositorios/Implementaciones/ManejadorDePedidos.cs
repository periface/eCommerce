using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.TransaccionBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PymeTamFinal.Repositorios.Implementaciones
{
    /// <summary>
    /// Manejador de pedidos, guarda los pedidos de manera predeterminada usando entity framework
    /// </summary>
    public class ManejadorDePedidos : TransaccionBase<CompraModel>
    {
        private const string idContext = "ordenCtx";
        public ManejadorDePedidos(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override void guardarOrden(CompraModel orden, string cartId, string idUsuario, decimal descuento, object httpContext)
        {
            //Existe un error al agregar una orden con cupón
            var model = new Orden();
            var carroItems = context.CarritoCompra.Where(a => a.idCarro == cartId).ToList();
            var usuario = context.Cliente.Where(a => a.idAsp == idUsuario).SingleOrDefault();
            var envio = context.CostosEnvio.SingleOrDefault(a => a.idEnvio == orden.idEnvio);
            if (envio == null)
            {
                return;
            }
            model.ordenNombreEnvio = envio.nombreMetodoEnvio;
            model.costoEnvio = envio.costo;
            if (descuento < 0 && !string.IsNullOrEmpty(orden.cupon))
            {
                var descuentoDb = context.CuponDescuento.SingleOrDefault(a => a.codigoCupon == orden.cupon);
                if (descuentoDb != null)
                {
                    model.ordenDescuento = descuento;
                    model.ordenTipoDescuento = descuentoDb.tipoDesc;
                    model.ordenCodigoCupon = descuentoDb.codigoCupon;
                    model.ordenSubTotal = orden.total;
                    model.ordenTotal = orden.total;
                    //El descuento debe ser negativo para hacer efecto
                    model.ordenTotal += descuento;
                    model.ordenTotal += model.costoEnvio;
                }
            }
            else
            {
                model.ordenSubTotal = orden.total;
                model.ordenTotal += model.costoEnvio;
                model.ordenTotal += orden.total;
            }
            model.ordenMail = orden.email;
            model.ordenDireccionLinea1 = usuario.direccionEnvioLinea1;
            model.ordenDireccionLinea2 = usuario.direccionEnvioLinea2;
            model.ordenEstado = context.Estados.Find(orden.idEstado).nombreEstado;
            model.ordenPais = context.Pais.Find(orden.idPais).nombrePais;
            model.ordenCiudad = usuario.ciudad;
            model.enviarADirFac = orden.enviarDireccionEnvio;
            model.idCliente = usuario.idCliente;
            model.ordenApMaterno = usuario.apMaterno;
            model.ordenApPaterno = usuario.apPaterno;
            model.ordenClienteComentarios = orden.detalles;
            model.ordenCodigoCupon = orden.cupon;
            model.ordenCodigoPostal = usuario.cp;
            model.ordenDireccionFACLinea1 = usuario.direccionFacturacionLinea1;
            model.ordenDireccionFACLinea2 = usuario.direccionFacturacionLinea2;
            model.ordenFecha = DateTime.Now;
            model.ordenFechaPago = null;
            model.ordenNombre = usuario.nombre;
            model.ordenPagado = false;
            model.ordenRevisado = false;
            model.ordenRfc = usuario.rfc;
            model.ordenTelefono = usuario.telefono;
            model.ordenTipoPago = orden.pago;
            model.ordenEstadoPedido = "Pendiente";
            model.razonSocial = usuario.razonSocial;
            model.requiereFactura = orden.requiereFactura;
            context.Orden.Add(model);
            //Guardamos la orden
            context.SaveChanges();
            foreach (var item in carroItems)
            {
                var producto = context.Producto.SingleOrDefault(a => a.idProducto == item.idProducto);
                var detalleOrden = new OrdenDetalle()
                {
                    cantidad = item.contadorCarro,
                    idOrden = model.idOrden,
                    idProducto = item.idProducto,
                    nombreProducto = producto.nombreProducto,
                    precioUnitario = cargaPrecio(producto),
                    total = cargaTotalRecord(item),
                    sku = producto.sku
                };
                context.OrdenDetalle.Add(detalleOrden);
            }
            context.SaveChanges();
            guardaContexto(httpContext, model.idOrden);
        }
        private void guardaContexto(object context, object contextId)
        {
            var ctx = (HttpContextBase)context;
            ctx.Session[idContext] = contextId;
        }
        public override void limpiaContexto(object context)
        {
            var ctx = (HttpContextBase)context;
            ctx.Session[idContext] = null;
        }
        public override int cargaContexto(object context)
        {
            var ctx = (HttpContextBase)context;
            if (ctx.Session[idContext] != null)
            {
                return Convert.ToInt32(ctx.Session[idContext]);
            }
            return 0;
        }
        private decimal cargaTotalRecord(CarritoDeCompra item)
        {
            var precio = context.Precios.SingleOrDefault(a => a.idProducto == item.idProducto);
            if (precio.descuentoActivo)
            {
                return item.contadorCarro * precio.precioEsp;
            }
            else
            {
                return item.contadorCarro * precio.precio;
            }
        }

        private decimal cargaPrecio(Producto producto)
        {
            var precio = context.Precios.SingleOrDefault(a => a.idProducto == producto.idProducto);
            if (precio.descuentoActivo)
            {
                return precio.precioEsp;
            }
            else
            {
                return precio.precio;
            }
        }
    }
}
