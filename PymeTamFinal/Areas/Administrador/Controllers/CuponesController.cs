using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.HtmlHelpers.MensajeServicio;
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
        IRepositorioBase<Cliente> _clientes;
        public CuponesController(IRepositorioBase<CuponDescuento> _descuentos,IRepositorioBase<Cliente>_clientes)
        {
            this._descuentos = _descuentos;
            this._clientes = _clientes;
        }
        // GET: Administrador/Cupones
        public ActionResult Index()
        {
            ServicioDeMensajes.obtieneMensaje(ControllerContext.Controller);
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
            return View(cupones);
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
            if (!idCliente.HasValue)
                return "Sin cliente.";
            var cliente = _clientes.CargarPorId(idCliente);
            if (cliente == null)
                return "Cliente no encontrado";
            return cliente.nombreCliente;
        }
        public ActionResult NuevoCupon() {
            ViewBag.usuarios = _clientes.Cargar();
            ViewBag.tipos = cargaTipos;
            return View();
        }
        [HttpPost]
        public ActionResult NuevoCupon(CuponDescuento model) {
            if (existeCupon(model.codigoCupon)) {
                ModelState.AddModelError(string.Empty,"Este cupon ya existe");
                ViewBag.usuarios = _clientes.Cargar();
                ViewBag.tipos = cargaTipos;
                return View(model);
            }
            if (ModelState.IsValid) {
                _descuentos.Agregar(model);
                ServicioDeMensajes.darMensaje(ServicioDeMensajes.enumMensaje.Agregado,ControllerContext.Controller);
                return RedirectToAction("Index");
            }
            ViewBag.usuarios = _clientes.Cargar();
            ViewBag.tipos = cargaTipos;
            return View(model);
        }
        private bool existeCupon(string codigo) {
           var cupon =  _descuentos.Cargar(a => a.codigoCupon == codigo).SingleOrDefault();
            if (cupon == null)
            {
                return false;
            }
            else {
                return true;
            }
        }
        private List<tipo> tipos = new List<tipo>();
        private SelectList cargaTipos
        {
            get
            {
                tipos.Add(new tipo
                {
                    nombre = "Real",

                });
                tipos.Add(
                new tipo
                {
                    nombre = "Porcentual"
                });
                return new SelectList(tipos, "nombre", "nombre");

            }
        }
        private class tipo
        {
            int id { get; set; }
            public string nombre { get; set; }
        }
    }
}