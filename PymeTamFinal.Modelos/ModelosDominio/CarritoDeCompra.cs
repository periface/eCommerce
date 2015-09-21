using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("CarritoDeCompra")]
    public class CarritoDeCompra
    {
        [Key]
        public int idRecord { get; set; }
        [MaxLength(50)]
        public string idCarro { get; set; }
        public int idProducto { get; set; }
        public int contadorCarro { get; set; }
        public DateTime fechaCreacion { get; set; }
        [ForeignKey("idProducto")]
        public virtual Producto producto { get; set; }
    }
}
