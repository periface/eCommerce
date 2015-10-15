using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class ComentarioTablaViewModel
    {
        public int idCliente { get; set; }
        public string cliente { get; set; }
        public string producto { get; set; }
        public string calificacion { get; set; }
        public string opciones { get; set; }
        public string fecha { get; set; }
    }
}
