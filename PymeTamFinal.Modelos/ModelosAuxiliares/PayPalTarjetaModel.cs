using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class payPalTarjetaModel
    {
        public string numero { get; set; }
        public string tipo { get; set; }
        public string mesExp { get; set; }
        public string anoExp { get; set; }
        public string cvv2 { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string direccionLinea1 { get; set; }
        public string ciudad { get; set; }
        public int idPais { get; set; }
        public string codigoPostal { get; set; }
        public int idEstado { get; set; } 
        public string email { get; set; }
        public bool guardar { get; set; }
    }
}
