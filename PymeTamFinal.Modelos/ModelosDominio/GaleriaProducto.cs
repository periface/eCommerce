using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("GaleriaProducto")]
    public class GaleriaProducto
    {
        [Key]
        public int idImagen { get; set; }
        public int idProducto { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string imagen { get; set; }
        [ForeignKey("idProducto")]
        public virtual Producto producto { get; set; }
    }
}
