using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class ReciboViewModel
    {
        public string nombreEmpresa { get; set; }
        public string logoEmpresa { get; set; }
        public int idOrden { get; set; }
        public string fecha { get; set; }
        public Cliente cliente { get; set; }
        public CompraModel orden { get; set; }
        public string telefonoEmpresa { get; set; }
        public string emailEmpresa { get; set; }
        public string direccionEmpresa { get; set; }
        public string razonSocialEmpresa { get; set; }
    }
}
