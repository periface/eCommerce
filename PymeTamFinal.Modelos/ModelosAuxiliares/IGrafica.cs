using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class IGrafica
    {
        public rangeSelector rangeSelector { get; set; }
        public title title { get; set; }
    }
    public class rangeSelector
    {
       public  int selected { get; set; }
    }
    public class title
    {
        public string text { get; set; }
    }
}
