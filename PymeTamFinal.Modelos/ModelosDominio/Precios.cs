using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Precios")]
    public class Precios
    {
        [Key]
        public int idPrecio { get; set; }
        public int idProducto { get; set; }
        /// <summary>
        /// precio original
        /// </summary>
        public decimal precio { get; set; }
        /// <summary>
        /// Descuento aplicado al precio original
        /// </summary>
        public decimal descuento { get; set; }
        /// <summary>
        /// precio con el descuento aplicado
        /// </summary>
        public decimal precioEsp { get; set; }
        public string tipo { get; set; }
        public bool descuentoActivo { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaVencimiento { get; set; }
    }
}
