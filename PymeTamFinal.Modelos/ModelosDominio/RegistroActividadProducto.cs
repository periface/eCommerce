using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("RegistroDeActividad")]
    public class RegistroActividadProducto
    {
        [Key]
        public int idRegistro { get; set; }
        public int? idProducto { get; set; }
        public string ip { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
        public string nombreUsuario { get; set; }
        public int edadUsuario { get; set; }
    }
}
