using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Pais")]
    public class Pais
    {
        [Key]
        public int idPais { get; set; }
        public string nombrePais { get; set; }
        public string langCode { get; set; }
        public ICollection<Estados> estados { get; set; }
    }
}
