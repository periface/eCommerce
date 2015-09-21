using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class ProductoTablaViewModel
    {
        public int idProducto { get; set; }
        public string categoria { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public string precioProducto { get; set; }
        public DateTime fechaCreacion { get; set; }
        public bool habilitado { get; set; }
        public string imgProducto { get; set; }
        public string descripcionCorta { get; set; }
        public string tags { get; set; }
        public bool habilitarComentarios { get; set; }
        public string fechaModificacion { get; set; }
        public string slugs { get; set; }
        public int stock { get; set; }
        public bool mostrarStock { get; set; }
        public bool mostrarSinStock { get; set; }
        public bool inhabilitarCompraSinStock { get; set; }
        public string opciones { get; set; }
        public string sku { get; set; }
        public string precio { get; set; }
        public string precioDescuentoActivo { get; set; }
    }
}
