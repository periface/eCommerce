using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("CostosEnvio")]
    public class CostosEnvio
    {
        [Key]
        public int idEnvio { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string nombreMetodoEnvio { get; set; }
        [Required(ErrorMessage = "Costo requerido")]
        [DataType(DataType.Currency,ErrorMessage ="Costo no valido")]
        public decimal costo { get; set; }
        [Required(ErrorMessage = "Detalle requerido")]
        public string detalle { get; set; }
    }
}
