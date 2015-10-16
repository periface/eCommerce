using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public int idEmpresa { get; set; }
        [MaxLength(100,ErrorMessage ="No mas de {1} caracteres")]
        [Required(ErrorMessage = "Nombre requerido")]
        public string nombreEmpresa { get; set; }
        [Required(ErrorMessage = "Misión requerida")]
        [MaxLength(400, ErrorMessage = "No mas de {1} caracteres")]
        public string misionEmpresa { get; set; }
        [Required(ErrorMessage = "Visión requerida")]
        [MaxLength(400, ErrorMessage = "No mas de {1} caracteres")]
        public string visionEmpresa { get; set; }
        [Required(ErrorMessage = "Dirección requerida")]
        [MaxLength(200, ErrorMessage = "No mas de {1} caracteres")]
        public string direccionEmpresa { get; set; }
        [Required(ErrorMessage = "Telefono requerido")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Telefono no valido")]
        public string telefonoEmpresa { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido")]
        [Required(ErrorMessage = "Correo requerido")]
        public string correoOficialEmpresa { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido")]
        [Required(ErrorMessage = "Correo requerido")]
        public string correoVentasEmpresa { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido")]
        [Required(ErrorMessage = "Correo requerido")]
        public string correoAyudaEmpresa { get; set; }
        [Required(ErrorMessage = "Correo requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Correo no valido")]
        public string correoEnviosEmpresa { get; set; }
        public string facebookEmpresa { get; set; }
        public string facebookId { get; set; }
        public string twitterEmpresa { get; set; }
        public string twitterId { get; set; }
        public bool infoActiva { get; set; }
        public string slogan { get; set; }
        public string imgPrincipalEmpresa { get; set; }
        public string empLat { get; set; }
        public string empLong { get; set; }
        public string razonSocial { get; set; }
    }
}
