using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class CuponesController : Controller
    {
        IRepositorioBase<CuponDescuento> _descuentos;
        public CuponesController(IRepositorioBase<CuponDescuento> _descuentos)
        {
            this._descuentos = _descuentos;
        }
        // GET: Administrador/Cupones
        public ActionResult Index()
        {
            var cupones = new List<CuponTablaViewModel>();
            foreach (var item in _descuentos.Cargar())
            {
                cupones.Add(new CuponTablaViewModel {
                    cantidadesUso = item.cantidadesUso,
                    codigoCupon = item.codigoCupon,
                    descuento = item.descuento,
                    cliente = cargaCliente(item.idCliente),
                    fechaCaducidad = item.fechaCaducidad,
                    idCupon = item.idCupon,
                    minimoRequerido = item.minimoRequerido,
                    tipoDesc = item.tipoDesc,
                    usado = cargaEstado(item.usado),
                    usoEnDescuentos = cargaCondicion(item.usoEnDescuentos)
                });
            }
            return View();
        }

        private string cargaCondicion(bool usoEnDescuentos)
        {
            if (usoEnDescuentos)
            {
                return "Habilitado en otras promociones";
            }
            else {
                return "Deshabilitado en otras promociones";
            }
        }
        private string cargaEstado(bool usado)
        {
            if (usado)
            {
                return "Cupon usado";
            }
            else {
                return "No usado";
            }
        }
        private string cargaCliente(int? idCliente)
        {
            throw new NotImplementedException();
        }
        public ActionResult NuevoCupon() {
            return View();
        }
    }
}