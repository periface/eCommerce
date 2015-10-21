using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.CheckOut.Models
{
    public class metodoPagoPayPal : IMetodoPago
    {
        private int idOrden;
        private readonly IOrdenGeneradorBase<compraModel> _orden;
        public metodoPagoPayPal(string rtname, int orden)
        {
            result = new RedirectResult(rtname);
            idOrden = orden;
        }
        public metodoPagoPayPal(string rtname)
        {
            result = new RedirectResult(rtname);
        }
        public metodoPagoPayPal(string rtname,int orden,IOrdenGeneradorBase<compraModel>_orden)
        {
            this._orden = _orden;
            result = new RedirectResult(rtname);
            idOrden = orden;
        }

        public override void guardarEstadistica()
        {
            
        }
    }
}
