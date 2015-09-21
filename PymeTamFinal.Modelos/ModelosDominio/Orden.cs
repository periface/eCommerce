using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Orden")]
    public class Orden
    {
        [Key]
        public int idOrden { get; set; }
        public int? idCliente { get; set; }
        public string ordenNombre { get; set; }
        public string ordenApPaterno { get; set; }
        public string ordenApMaterno { get; set; }
        public string ordenDireccion { get; set; }
        public string ordenPais { get; set; }
        public string ordenEstado { get; set; }
        public string ordenCiudad { get; set; }
        public string ordenCodigoPostal { get; set; }
        public string ordenTelefono { get; set; }
        public string ordenMail { get; set; }
        public decimal ordenTotal { get; set; }
        public DateTime ordenFecha { get; set; }
        public bool ordenRevisado { get; set; }
        public DateTime ordenFechaPago { get; set; }
        public bool ordenPagado { get; set; }
        public string ordenCodigoRastreo { get; set; }
        public string ordenCodigoCupon { get; set; }
        public decimal ordenSubTotal { get; set; }
        public string ordenCodigoPayPal { get; set; }
        public decimal ordenDescuento { get; set; }
        public string ordenNombreEnvio { get; set; }
        public string ordenTipoPago { get; set; } 
        public string ordenEstadoPedido { get; set; }
        public string ordenImagenTicket { get; set; }
        public string ordenBase64Imagen { get; set; }
        public string ordenComentarioEstado { get; set; }
        public virtual ICollection<OrdenDetalle> ordenDetalle { get; set; }
        [ForeignKey("idCliente")]
        public virtual Cliente cliente { get; set; }
    }
}
