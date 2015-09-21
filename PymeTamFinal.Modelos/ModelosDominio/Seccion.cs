using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Seccion")]
    public class Seccion
    {
        [Key]
        public int idSeccion { get; set; }
        [MaxLength(100,ErrorMessage ="No mas de {1} caracteres")]
        [Required(ErrorMessage ="Nombre de sección requerido")]
        public string nombreSeccion { get; set; }
        public bool estadoSeccion { get; set; }
        public string imagenSeccion { get; set; }
        [MaxLength(400, ErrorMessage = "No mas de {1} caracteres")]
        [Required(ErrorMessage = "Contenido de sección requerido")]
        public string contenidoSeccion { get; set; }
    }
}
