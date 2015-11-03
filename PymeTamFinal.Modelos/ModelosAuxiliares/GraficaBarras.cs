using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class GraficaBarras : IGrafica
    {
        public GraficaBarras()
        {
            xAxis = new xAxis();
        }
        public xAxis xAxis { get; set; }
        public seriesBarras[] series{ get; set; }
    }
    public class xAxis {
        public string[] categories { get; set; }
    }
    public class seriesBarras {
        public string name { get; set; }
        public string type { get; set; }
        public decimal[] data { get; set; }
    }
}
