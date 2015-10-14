using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    /// <summary>
    /// 
    /// </summary>
    //public class ModeloGuardadoCompra
    //{
    //    public string ordenNombre { get; set; }
    //    public string ordenApPaterno { get; set; }
    //    public string ordenApMaterno { get; set; }
    //    public string ordenDireccion { get; set; }
    //    public string ordenPais { get; set; }
    //    public string ordenEstado { get; set; }
    //    public string ordenCiudad { get; set; }
    //    public string ordenCodigoPostal { get; set; }
    //    public string ordenTelefono { get; set; }
    //    public string ordenMail { get; set; }
    //    public decimal ordenTotal { get; set; }
    //    public DateTime ordenFecha { get; set; }
    //    public bool revisado { get; set; }
    //    public DateTime fechaPago { get; set; }
    //    public bool pagado { get; set; }
    //    public string ordenCupon { get; set; }
    //    public decimal subTotal { get; set; }
    //    public string descuentoCupon { get; set; }
    //    public decimal valorDescuento { get; set; }
    //    public string tipoPago { get; set; }
    //    public string ordenEstadoPedido { get; set; }
    //    public string ordenImagenTicket { get; set; }
    //    public string ordenComentarioEstado { get; set; }
    //    public string ordenClienteComentarios { get; set; }
    //    public bool enviarADirFac { get; set; }
    //    public bool requiereFactura { get; set; }
    //}
    public class compraModel {
        public string cupon { get; set; }
        public bool requiereFactura { get; set; }
        public bool enviarDireccionEnvio { get; set; }
        public string detalles { get; set; }
        public int idEnvio { get; set; }
        public string pago { get; set; }
        public decimal total { get; set; }
        public int idPais { get; set; }
        public int idEstado { get; set; }
        public string email { get; set; }
    }
}
