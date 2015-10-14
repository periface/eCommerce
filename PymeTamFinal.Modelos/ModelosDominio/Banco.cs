using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Banco")]
    public class Banco
    {
        [Key]
        public int bancoId { get; set; }
        public string bancoNombre { get; set; }
        public string bancoNConvenio { get; set; }
        public string bancoReferencia { get; set; }
        public string bancoImagen { get; set; }
        public bool bancoActivo { get; set; }
    }
}
