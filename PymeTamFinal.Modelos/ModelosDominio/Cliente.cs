﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int idCliente { get; set; }
        public string idAsp { get; set; }
        [Required(ErrorMessage = "Usuario requerido")]
        [Remote("nombreDisponible","Clientes",ErrorMessage ="Nombre de usuario no disponible")]
        public string nombreUsuario { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Primer apellido requerido")]
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public string ciudad { get; set; }
        public int idEstado { get; set; }
        public int idPais { get; set; }
        [Required(ErrorMessage = "Linea 1 requerida")]
        public string direccionEnvioLinea1 { get; set; }
        public string direccionEnvioLinea2 { get; set; }
        public string razonSocial { get; set; }
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
        /// <summary>
        /// Solo si el usuario fue encontrado
        /// </summary>
        [NotMapped]
        public string nombreCliente {
            get { return string.Format("{0} {1} {2}",nombre,apPaterno,apMaterno); }
        }
    }
}
