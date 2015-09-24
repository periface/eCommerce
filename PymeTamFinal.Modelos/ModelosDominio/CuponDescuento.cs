using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    public class CuponDescuento
    {
        [Key]
        public int idCupon { get; set; }
        [MaxLength(50,ErrorMessage ="No mas de {1} caracteres")]
        public string codigoCupon { get; set; }
        public string tipoDesc { get; set; }
        public int? idCliente { get; set; }
        public decimal descuento { get; set; }
        public bool usado { get; set; }
        public int cantidadesUso { get; set; }
        public bool usoEnDescuentos { get; set; }
        public DateTime fechaCaducidad { get; set; }
        public decimal minimoRequerido { get; set; }
        [ForeignKey("idCliente")]
        public virtual Cliente cliente { get; set; }
    }
}
