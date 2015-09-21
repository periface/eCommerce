using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class CategoriaAdminEliminaViewModel
    {
        public CategoriaAdminEliminaViewModel()
        {
            categoria = new CategoriaAdminViewModel();
            hijos = new List<CategoriaAdminViewModel>();
        }
        public CategoriaAdminViewModel categoria
        {
            get; set;
        }
        public List<CategoriaAdminViewModel> hijos
        {
            get; set;
        }
    }
}
