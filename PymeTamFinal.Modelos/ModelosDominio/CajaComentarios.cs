using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("CajaComentarios")]
    public class CajaComentarios
    {
        [Key]
        public int idComentario { get; set; }
        public int? idCliente { get; set; }
        public string nombreCliente { get; set; }
        public int idProducto { get; set; }
        public int calificacion { get; set; }
        public bool revisado { get; set; }
        [Required(ErrorMessage ="Comentario requerido")]
        public string comentario { get; set; }
        [ForeignKey("idProducto")]
        public virtual Producto producto { get; set; }
        [ForeignKey("idCliente")]
        public virtual Cliente cliente { get; set; }

    }
}
