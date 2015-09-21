using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("Mutadores")]
    public class Mutadores
    {
        [Key]
        public int idMut { get; set; }
        //caracteristica Grande-Mediano-Chico L-M-S-XS 
        public string caract { get; set; }
        //Valor agregado que le da al producto
        //si es cero el precio no es afectado
        //de ser porcentual se calcula el nuevo precio acorde al precio original del producto
        public decimal valorAgregado { get; set; }
        //Obligatorio tener un mutador por defecto, para que empiece seleccionado
        public bool defecto { get; set; }
        public bool porcentual { get; set; }
        public int idDiferenciador { get; set; }
        [ForeignKey("idDiferenciador")]
        public virtual Diferenciadores diferenciador { get; set; }
    }
}