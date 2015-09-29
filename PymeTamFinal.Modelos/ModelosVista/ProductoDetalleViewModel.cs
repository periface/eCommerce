using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class ProductoDetalleViewModel
    {
        public bool cajaComentarios { get; set; }
        public decimal precio { get; set; }
        public string descripcionCorta { get; set; }
        public string detalle { get; set; }
        public int stock { get; set; }
        public bool mostrarStock { get; set; }
        public bool disponibleSinStock { get; set; }
        public string imagen { get; set; }
        public int idProducto { get; set; }
        public int promedio { get; set; }
        public string slug { get; set; }
        public string tags { get; set; }
        public int totalComents { get; set; }
        public string urlImg { get; set; }
        public string nombreProducto { get; set; }
    }
}
