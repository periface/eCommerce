using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class DetalleEnvioViewModel
    {
        public CostosEnvio costoEnvio { get; set; }
        public IEnumerable<Estados> estados{ get; set; }
    }
}
