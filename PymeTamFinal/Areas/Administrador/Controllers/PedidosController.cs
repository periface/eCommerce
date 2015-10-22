﻿using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosAuxiliares;
using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class PedidosController : AdminController
    {
        IRepositorioBase<Orden> _orden;
        IGeneradorGraficas<Orden> _ordenGraficas;
        //IGeneradorGraficas<GraficaPastel> _estados;
        public PedidosController(IRepositorioBase<Orden> _orden, IGeneradorGraficas<Orden> _ordenGraficas)
        {
            this._orden = _orden;
            this._ordenGraficas = _ordenGraficas;
        }
        // GET: Administrador/Pedidos
        public ActionResult Index()
        {
            return View(_orden.Cargar());
        }
        public ActionResult AgregarCodigoRastreo(int? id)
        {
            return View();
        }
        public ActionResult MarcarComoPagado(int? id)
        {
            return View();
        }
        public ActionResult CancelarPedido(int? id)
        {
            return View();
        }
        public ActionResult EditarEstado(int? id)
        {
            return View();
        }
        public ActionResult VerDetalles(int? id)
        {
            return View();
        }
        public ActionResult InvalidarTicket(int? id)
        {
            return View();
        }
        public ActionResult ValidarTicket(int? id)
        {
            return View();
        }
        public JsonResult VolumenVentas()
        {
            var pedidos = _orden.Cargar();
            var graficas = _ordenGraficas.generarGrafica(pedidos);
            return Json(graficas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Estados() {
            var estados =_ordenGraficas.generarGraficaAgrupacion("ordenEstadoPedido");
            return Json((GraficaPastel)estados,JsonRequestBehavior.AllowGet);
        }
        public ActionResult TiposPago()
        {
            var estados = _ordenGraficas.generarGraficaAgrupacion();
            return Json((GraficaPastel)estados, JsonRequestBehavior.AllowGet);
        }
        //private List< Dictionary<string, int>> generarDiccionario(IQueryable<Orden> pedidos)
        //{
        //    var lista = pedidos.ToList();
        //    var dic = new List<Dictionary<string, int>>();

        //    for (int i = 0; i < lista.Count; i++)
        //    {
        //        int val1 = Convert.ToInt16(lista[i].ordenTotal);
        //        string val2 = lista[i].ordenFecha.ToShortDateString();
        //        var valores = new Dictionary<string, int>();
        //        valores.Add(val2,val1);
        //        dic.Add(valores);
        //    }
        //    return dic;
        //}
    }
}