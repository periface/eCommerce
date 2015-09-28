using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("archivosEstilo")]
    public class archivosEstilo
    {
        [Key]
        public int idArchivo { get; set; }
        public int orden { get; set; }
        public string archivo { get; set; }
        public string comentarios { get; set; }
        public virtual DisenoSitio disenoSitio { get; set; }
    }
}