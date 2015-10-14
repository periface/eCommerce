using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class paypalPagoClienteModel
    {
        public string tipoMoneda { get; set; }
        public int pedido { get; set; }
        public string paymentId { get; set; }
        public string payerId { get; set; }
        public string cancelUrl { get; set; }
        public string returnUrl { get; set; }
    }
}
