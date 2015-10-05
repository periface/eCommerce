using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PymeTamFinal.Modelos.ModelosDominio;
using System.Web.Mvc;

namespace PymeTamFinal.MetodosPago.CarritoCompra
{
    public class CarroCompras
    {
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
            else {
                if (cantidad > 1)
                {

                    producto.stock = producto.stock - cantidad;
                    item.contadorCarro = item.contadorCarro + cantidad;
                }
                else {
                    item.contadorCarro++;
                    producto.stock--;
                }
            }
            db.SaveChanges();
        }
        public int RemoverDelCarro(int id) {
            var item = db.CarritoCompra.Include("producto").SingleOrDefault(a => a.idCarro == idCarro && a.idRecord == id);
            int conteoItem = 0;
            if (item != null) {
                if (item.contadorCarro > 1)
                {
                    item.contadorCarro--;
                    conteoItem = item.contadorCarro;
                    item.producto.stock++;
                }
                else {
                    item.producto.stock++;
                    db.CarritoCompra.Remove(item);
                    
                }
            }
            db.SaveChanges();
            return conteoItem;
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
