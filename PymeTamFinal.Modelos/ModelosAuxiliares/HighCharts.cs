using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class HighCharts : IGrafica
    {
        public HighCharts()
        {
            chart = new chart();
        }
        public string plotBackgroundColor { get; set; }
        public string plotBorderWidth { get; set; }
        public chart chart { get; set; }
        
    }
    public class chart
    {
        public string type { get; set; }
        public bool plotShadow { get; set; }
    }
    

}
