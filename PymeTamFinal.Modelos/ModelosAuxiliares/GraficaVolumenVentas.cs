using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class GraficaVolumenVentas : IGrafica
    {
        public List<series> series { get; set; }
    }
    public class series
    {
        public string type { get; set; }
        public string name { get; set; }
        public double[][] data { get; set; }
    }
    public class tooltip
    {
        public int valueDecimals { get; set; }
    }
    
}
