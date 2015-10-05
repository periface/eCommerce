using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosVista
{
    public class CarroDetalleViewModel
    {
        public List<CarritoDeCompra> items { get; set; }
        public decimal total { get; set; }
        public decimal subTotal { get; set; }

    }
}
