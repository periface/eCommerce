using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class paypalModel
    {
        public bool habilitada { get; set; }
        public string secret { get; set; }
        public string appId { get; set; }
        public string emailCuenta { get; set; }
    }
}
