using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Tarjetas")]
    public class Tarjetas
    {
        [Key]
        public string idTarjeta { get; set; }
        public int idCliente { get; set; }
        [ForeignKey("idCliente")]
        public virtual Cliente cliente { get; set; }
    }
}
