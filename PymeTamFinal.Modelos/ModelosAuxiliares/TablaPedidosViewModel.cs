using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class TablaPedidosViewModel
    {
        public int idOrden { get; set; }
        public string cliente { get; set; }
        public string ordenFecha { get; set; }
        public string ordenHora { get; set; }
        public string ordenTotal { get; set; }
        public string ordenEstadoPedido { get; set; }
        public string ordenTipoPago { get; set; }
        public string ordenPagado { get; set; }
        public string ordenDetalle { get; set; }
    }
}
