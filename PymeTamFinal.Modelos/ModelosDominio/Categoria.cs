using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        [MaxLength(200,ErrorMessage ="No mas de 200 caracteres")]
        [Required(ErrorMessage ="Nombre de categoria requerido")]

        public string nombreCategoria { get; set; }
        public int idPadre { get; set; }
        public virtual ICollection<Producto> productos { get; set; }
    }
}
