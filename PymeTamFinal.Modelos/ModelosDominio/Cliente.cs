using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        [Required(ErrorMessage = "Usuario requerido")]
        public string nombreUsuario { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Primer apellido requerido")]
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public int idCiudad { get; set; }
        [Required(ErrorMessage = "Linea 1 requerida")]
        public string direccionEnvioLinea1 { get; set; }
        public string direccionEnvioLinea2 { get; set; }
        public string direccionFacturacionLinea1 { get; set; }
        public string direccionFacturacionLinea2 { get; set; }
        public string rfc { get; set; }
        [DataType(DataType.PhoneNumber,ErrorMessage ="Telefono no valido")]
        public string telefono { get; set; }
        [DataType(DataType.PostalCode,ErrorMessage ="Codigo postal no valido")]
        [Required(ErrorMessage = "Codigo postal requerido")]
        public string cp { get; set; }
        public bool facturacionCapturada { get; set; }
        public bool datosCapturados { get; set; }
        [Required(ErrorMessage = "Edad requerida")]
        public int edad { get; set; }
        public virtual ICollection<Orden> ordenes { get; set; }
    }
}
