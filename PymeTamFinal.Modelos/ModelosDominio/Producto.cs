using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }
        public int? idCategoria { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool habilitado { get; set; }
        public string imgProducto { get; set; }
        public string descripcionCorta { get; set; }
        public string tags { get; set; }
        public bool habilitarComentarios { get; set; }
        public DateTime fechaModificacion { get; set; }
        public string slugs { get; set; }
        public int stock { get; set; }
        public bool mostrarStock { get; set; }
        public bool mostrarSinStock { get; set; }
        public bool habilitarCompraSinStock { get; set; }
        public string sku { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria categoria { get; set; }
        public virtual ICollection<CajaComentarios> comentarios { get; set; }
        public virtual ICollection<GaleriaProducto> galeria { get; set; }
        public virtual ICollection<Diferenciadores> diferenciadores { get; set; }
        public virtual Precios precio { get; set; }
    }
}
