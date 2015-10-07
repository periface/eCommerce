using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PymeTamFinal.Modelos.ModelosDominio;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PymeTamFinal.MetodosPago.CarritoCompra
{
    /// <summary>
    /// Todas las operaciónes de disminución de stock son efectuadas aqui
    /// </summary>
    public class CarroCompras
    {
        public enum mensajes
        {
            noMinCumplido,
            cuponCaducado,
            cuponUsado,
            cuponSoloUsuario,
            cuponNoEncontrado,
            cuponOk
        }
        string idCarro;
        CapaDatos.DataContext db = new CapaDatos.DataContext();
        private const string llaveSesion = "idCarro";
        public static CarroCompras _CarroCompras(HttpContextBase context)
        {
            var carro = new CarroCompras();
            carro.idCarro = carro.cargaId(context);
            return carro;
        }
        public static CarroCompras _CarroCompras(Controller controller)
        {
            return _CarroCompras(controller);
        }
        public void AgregaUno(int id)
        {
            var record = db.CarritoCompra.Include("producto").SingleOrDefault(a => a.idRecord == id);
            record.contadorCarro++;
            record.producto.stock--;
            db.SaveChanges();
        }
        public void EliminaUno(int id)
        {
            var record = db.CarritoCompra.Include("producto").SingleOrDefault(a => a.idRecord == id);
            record.contadorCarro--;
            record.producto.stock++;
            if (record.contadorCarro <= 0)
            {
                db.CarritoCompra.Remove(record);
            }
        }

        public mensajes AgregarCupon(string cupon, HttpContextBase controller, out decimal descuento)
        {
            /*
            VALIDA QUE SE ENCUENTRE
            VALIDA QUE SEA DEL USUARIO
                VALIDA QUE CUMPLA CON EL MINIMO
                VALIDA QUE NO ESTE USADO
                VALIDA QUE NO ESTE CADUCADO
            */
            descuento = 0;
            var cupondb = db.CuponDescuento.Include("cliente").SingleOrDefault(a => a.codigoCupon == cupon);
            var carro = _CarroCompras(controller);
            //Valida que se encuentre
            if (cupondb == null)
            {
                return mensajes.cuponNoEncontrado;
            }
            else
            {
                var id = aspIdCliente(controller);
                var cliente = db.Cliente.SingleOrDefault(a => a.idAsp == id);
                //Sabemos si tiene o no tiene un usuario
                if (cupondb.idCliente.HasValue)
                {
                    //Valida que sea del usuario
                    if (cupondb.idCliente == cliente.idCliente)
                    {
                        //Revisa si cumple con el minimo
                        if (CumpleConElMinimo(cupondb, carro.cargaTotal()))
                        {
                            //Revisa si no esta usado
                            if (CuponDisponible(cupondb))
                            {
                                //Revisa que no este caducado
                                if (CuponCaducado(cupondb))
                                {
                                    return mensajes.cuponCaducado;
                                }
                                else
                                {
                                    //No se me ocurre una forma de refactorizarlo
                                    //Esta función es el objetivo final lo llamaremos princesa!
                                    //Canción de mario bross pliss
                                    switch (cupondb.tipoDesc)
                                    {
                                        case "Real":
                                            descuento = Convert.ToDecimal(cupondb.descuento) * -1;
                                            return mensajes.cuponOk;
                                        case "Porcentual":
                                            var total = carro.cargaTotal();
                                            var valorCupon = Convert.ToDecimal(cupondb.descuento);
                                            descuento = (valorCupon * total / 100) * -1;
                                            return mensajes.cuponOk;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                return mensajes.cuponUsado;
                            }
                        }
                        else
                        {
                            return mensajes.noMinCumplido;
                        }

                    }
                    else
                    {
                        return mensajes.cuponNoEncontrado;
                    }
                }
                else {
                    //El cupon no esta asignado a ningun usuario(es libre)
                    /*
                    Mismo proceso
                    VALIDA QUE SE ENCUENTRE
                        VALIDA QUE CUMPLA CON EL MINIMO
                        VALIDA QUE NO ESTE USADO
                        VALIDA QUE NO ESTE CADUCADO
                    */
                    if (CumpleConElMinimo(cupondb, carro.cargaTotal()))
                    {
                        if (CuponDisponible(cupondb))
                        {
                            if(CuponCaducado(cupondb)){
                                return mensajes.cuponCaducado;
                            }
                            else{
                                //No se me ocurre una forma de refactorizarlo
                                //Esta función es el objetivo final lo llamaremos princesa!
                                //Canción de mario bross pliss
                                switch (cupondb.tipoDesc)
                                {
                                    case "Real":
                                        descuento = Convert.ToDecimal(cupondb.descuento) * -1;
                                        return mensajes.cuponOk;
                                    case "Porcentual":
                                        var total = carro.cargaTotal();
                                        var valorCupon = Convert.ToDecimal(cupondb.descuento);
                                        descuento = (valorCupon * total / 100) * -1;
                                        return mensajes.cuponOk;
                                    default:
                                        break;
                                }
                            }
                        }
                        else {
                            return mensajes.cuponUsado;
                        }
                    }
                    else {
                        return mensajes.noMinCumplido;
                    }
                }
            }
            return mensajes.cuponNoEncontrado;
        }

        private bool CuponCaducado(CuponDescuento cupondb)
        {
            if (cupondb.fechaCaducidad > DateTime.Now) {
                return false;
            }
            return true;
        }

        private bool CuponDisponible(CuponDescuento cupondb)
        {
            if (cupondb.cantidadesUso <= 0 && cupondb.usado) {
                return false;
            }
            if (cupondb.cantidadesUso >= 1 && !cupondb.usado) {
                return true;
            }
            return false;
        }

        private bool CumpleConElMinimo(CuponDescuento cupondb,decimal total)
        {
            if (cupondb.minimoRequerido < total)
            {
                return true;
            }
            else {
                return false;
            }
        }

        private string aspIdCliente(HttpContextBase controller)
        {
            return controller.User.Identity.GetUserId();
        }
        public void EliminarRecord(int id)
        {
            var record = db.CarritoCompra.SingleOrDefault(a => a.idProducto == id);
            db.CarritoCompra.Remove(record);

            db.SaveChanges();
        }
        public void AgregarAlCarro(int idProducto, int cantidad = 1)
        {
            var producto = db.Producto.SingleOrDefault(a => a.idProducto == idProducto);
            var item = db.CarritoCompra.SingleOrDefault(a => a.idCarro == idCarro && a.idProducto == producto.idProducto);
            if (item == null)
            {
                item = new CarritoDeCompra()
                {
                    contadorCarro = cantidad,
                    fechaCreacion = DateTime.Now,
                    idCarro = idCarro,
                    idProducto = producto.idProducto
                };
                producto.stock--;
                db.CarritoCompra.Add(item);
            }
            else
            {
                if (cantidad > 1)
                {

                    producto.stock = producto.stock - cantidad;
                    item.contadorCarro = item.contadorCarro + cantidad;
                }
                else
                {
                    item.contadorCarro++;
                    producto.stock--;
                }
            }
            db.SaveChanges();
        }
        public int RemoverDelCarro(int id)
        {
            var item = db.CarritoCompra.Include("producto").SingleOrDefault(a => a.idCarro == idCarro && a.idRecord == id);
            int conteoItem = 0;
            if (item != null)
            {
                if (item.contadorCarro > 1)
                {
                    item.contadorCarro--;
                    conteoItem = item.contadorCarro;
                    item.producto.stock++;
                }
                else
                {
                    item.producto.stock++;
                    db.CarritoCompra.Remove(item);

                }
            }
            db.SaveChanges();
            return conteoItem;
        }
        public CarritoDeCompra cargaRecord(int idRecord)
        {
            return db.CarritoCompra.SingleOrDefault(a => a.idRecord == idRecord);
        }
        public List<CarritoDeCompra> cargaItems()
        {
            return db.CarritoCompra.Include("producto").Where(carro => carro.idCarro == idCarro).ToList();
        }
        public decimal cargaTotal()
        {
            decimal? total = 0;
            var carro = db.CarritoCompra.Include("producto").Where(a => a.idCarro == idCarro);
            foreach (var item in carro)
            {
                total = total + cargaPrecio(item);
            }
            return total ?? decimal.Zero;
        }
        /// <summary>
        /// Haber si jala!!
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private int? cargaPrecio(CarritoDeCompra items)
        {
            decimal precio = 0;
            var _precio = db.Precios.Where(a => a.idProducto == items.idProducto).SingleOrDefault();
            if (_precio.descuentoActivo)
            {
                precio = _precio.precioEsp * items.contadorCarro;
            }
            else
            {
                precio = _precio.precio * items.contadorCarro;
            }
            return Convert.ToInt32(precio);
        }

        public string cargaId(HttpContextBase context)
        {
            if (context.Session[llaveSesion] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[llaveSesion] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Genera un id aleatorio
                    Guid tempCartId = Guid.NewGuid();
                    // Envia el id como cookie
                    context.Session[llaveSesion] = tempCartId.ToString();
                }
            }
            return context.Session[llaveSesion].ToString();
        }
    }
}
