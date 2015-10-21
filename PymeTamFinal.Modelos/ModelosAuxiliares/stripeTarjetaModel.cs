using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class stripeTarjetaModel
    {

        public string numero { get; set; }
        public string tipo { get; set; }
        public string mesExp { get; set; }
        public string anoExp { get; set; }
        public string cvv2 { get; set; }
        public string nombre { get; set; }
        public string direccionLinea1 { get; set; }
        public string direccionLinea2 { get; set; }
        public string ciudad { get; set; }
        public string pais { get; set; }
        string cvc { get; set; }
        public string codigoPostal { get; set; }
        public string estado { get; set; }
        public bool guardar { get; set; }
    }
}
