using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class PaginaProductosViewModel
    {
        public PaginaProductosViewModel()
        {
            _productos = new List<ProductoListaViewModel>();
            _categorias = new List<CategoriasMenuRapido>();
        }
        public List<ProductoListaViewModel> _productos { get; set; }
        public List<CategoriasMenuRapido> _categorias { get; set; }
    }
}
