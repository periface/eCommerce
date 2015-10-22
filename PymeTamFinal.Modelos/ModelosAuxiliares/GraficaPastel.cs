using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class GraficaPastel : HighCharts
    {
        public GraficaPastel()
        {
            series = new List<seriesPastel>();
        }
        public List<seriesPastel> series { get; set; }
    }
    public class seriesPastel
    {
        public seriesPastel()
        {
            data = new List<dataPastel>();
        }
        public string name { get; set; }
        public bool colorByPoint { get; set; }
        public List<dataPastel> data { get; set; }
        public string innerSize { get; set; }
    }
    public class dataPastel
    {
        public string name { get; set; }
        public double y { get; set; }
        public bool sliced { get; set; }
        public bool selected { get; set; }
    }
}
