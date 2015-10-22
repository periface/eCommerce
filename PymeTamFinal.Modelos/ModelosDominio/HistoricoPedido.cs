using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosDominio
{
    [Table("HistoricoPedido")]
    public class HistoricoPedido
    {
        [Key]
        public int idHistorico { get; set; }
        public string estado { get; set; }
        public string colorEstado { get; set; }
        public DateTime fecha { get; set; }
        public string comentarios { get; set; }
        public ICollection<Orden> ordenes { get; set; }
    }
}
