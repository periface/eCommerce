using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    /// <summary>
    /// Categoria para evitar errores de referencia circular
    /// </summary>
    public class CategoriaAdminViewModel
    {
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public int idPadre { get; set; }
    }
}
