using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Politicas")]
    public class Politicas
    {
        [Key]
        public int idPolitica { get; set; }
        public string nombrePolitica { get; set; }
        public string contenidoPolitica { get; set; }
        public string slugPolitica { get; set; }
        public DateTime fechaPublicacion { get; set; }
        public DateTime fechaActualizacion { get; set; }
    }
}
