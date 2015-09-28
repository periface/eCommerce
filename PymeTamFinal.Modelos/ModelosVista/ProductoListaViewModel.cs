using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class ProductoListaViewModel
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descCorta { get; set; }
        public string imagen { get; set; }
        public string slug { get; set; }
        public string precio { get; set; }
        public int calificacionProm { get; set; }
    }
}
