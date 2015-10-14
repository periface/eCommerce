using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("PaypalConfig")]
    public class PaypalConfig
    {
        [Key]
        public int idCuenta { get; set; }
        public bool habilitada { get; set; }
        public string secret { get; set; }
        public string appId { get; set; }
        public string emailCuenta { get; set; }
    }
}
