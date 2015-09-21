using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Diferenciadores")]
    public class Diferenciadores
    {
        [Key]
        public int idDiferenciador { get; set; }
        public string mensajeDiferenciador { get; set; }
        /// <summary>
        /// Radio,selectList,rango
        /// </summary>
        public string tipoDiferenciador { get; set; }
        public int idProducto { get; set; }
        [ForeignKey("idProducto")]
        public virtual Producto producto { get; set; }
        public virtual ICollection<Mutadores> mutadores { get; set; }
    }
}
