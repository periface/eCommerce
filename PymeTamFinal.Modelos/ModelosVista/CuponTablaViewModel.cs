using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class CuponTablaViewModel
    {
        public int idCupon { get; set; }
        public string codigoCupon { get; set; }
        public string tipoDesc { get; set; }
        public string cliente { get; set; }
        public decimal descuento { get; set; }
        public string usado { get; set; }
        public int cantidadesUso { get; set; }
        public string usoEnDescuentos { get; set; }
        public DateTime fechaCaducidad { get; set; }
        public decimal minimoRequerido { get; set; }
        public string opciones { get; set; }
    }
}
