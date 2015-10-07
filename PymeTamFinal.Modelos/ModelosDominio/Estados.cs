using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Estados")]
    public class Estados
    {
        [Key]
        public int idEstado { get; set; }
        [Required(ErrorMessage = "Nombre de estado requerido")]
        public string nombreEstado { get; set; }
        public int idPais { get; set; }
        [ForeignKey("idPais")]
        public virtual Pais pais { get; set; }
        //public virtual ICollection<Ciudad> ciudades {get;set;}
        public virtual ICollection<CostosEnvio> costosEnvio
        {
            get; set;
        }
    }
}