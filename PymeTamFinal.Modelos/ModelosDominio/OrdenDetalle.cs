using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("OrdenDetalle")]
    public class OrdenDetalle
    {
        [Key]
        public int idDetalle { get; set; }
        public int idOrden { get; set; }
        public int? idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal total { get; set; } 
        public string sku { get; set; }
        [ForeignKey("idOrden")]
        public virtual CompraModel orden { get; set; }
    }
}