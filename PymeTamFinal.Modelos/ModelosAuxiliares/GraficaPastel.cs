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
            series = new List<seriesPastel>();
        }
        public string plotBackgroundColor { get; set; }
        public string plotBorderWidth { get; set; }
        public chart chart { get; set; }
        public List<seriesPastel> series { get; set; }
    }
    public class chart
    {
        public string type { get; set; }
        public bool plotShadow { get; set; }
    }
    public class seriesPastel {
        public seriesPastel()
        {
            data = new dataPastel();
        }
        public string name { get; set; }
        public bool colorByPoint { get; set; }
        public dataPastel data { get; set; }
    }
    public class dataPastel {
        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }
    }

}
