using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Ciudad")]
    public class Ciudad
    {
        [Key]
        public int idCiudad { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string nombreCiudad { get; set; }
        public int idEstado { get; set; }
        [ForeignKey("idEstado")]
        public virtual Estados estado { get; set; }
        public virtual ICollection<CostosEnvio> costosEnvio
        {
            get; set;
        }
    }
}
