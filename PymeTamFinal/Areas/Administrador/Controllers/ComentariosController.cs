﻿using PymeTamFinal.Contratos.Repositorio;
using PymeTamFinal.Controles;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Modelos.ModelosVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PymeTamFinal.Areas.Administrador.Controllers
{
    public class ComentariosController : AdminController
    {
        IRepositorioBase<CajaComentarios> _comentarios;
        IRepositorioBase<Producto> _productos;
        IRepositorioBase<Cliente> _clientes;
        public ComentariosController(IRepositorioBase<CajaComentarios> _comentarios, IRepositorioBase<Producto> _productos, IRepositorioBase<Cliente> _clientes)
        {
            this._comentarios = _comentarios;
            this._productos = _productos;
            this._clientes = _clientes;
        }
        // GET: Administrador/Comentarios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CargaComentarios()
        {
            List<ComentarioTablaViewModel> tabla = new List<ComentarioTablaViewModel>();
            foreach (var item in _comentarios.Cargar())
            {
                tabla.Add(new ComentarioTablaViewModel()
                {
                    calificacion = generarCalificacion(item.calificacion),
                    idCliente = item.idCliente.Value,
                    opciones = generarOpciones(item.idComentario),
                    producto = generarInfoProducto(item.idProducto),
                    cliente = generaCliente(item.idCliente)
                });
            }
            return Json(tabla,JsonRequestBehavior.AllowGet);
        }

        private string generaCliente(int? idCliente)
        {
            var cliente = _clientes.CargarPorId(idCliente);
            string nombre = string.Format("{0} {1} {2}", cliente.nombre, cliente.apPaterno, cliente.apPaterno);
            return nombre;
        }

        private string generarInfoProducto(int idProducto)
        {
            TagBuilder a = new TagBuilder("a");
            var producto = _productos.CargarPorId(idProducto);
            if (producto != null)
            {
                a.SetInnerText(producto.nombreProducto);
                a.MergeAttribute("href", Url.Action("EditarProducto", "Productos", new { id = producto.idProducto }));
            }
            else
            {
                a.SetInnerText("No asignado");
            }
            a.MergeAttribute("target", "_blank");
            return a.ToString();
        }
        private string generarOpciones(int idComentario)
        {
            Dictionary<string, string> attrCompartidos = new Dictionary<string, string>();
            attrCompartidos.Add("href","#");
            attrCompartidos.Add("data-id", idComentario.ToString());
            TagBuilder aEliminar = new TagBuilder("a");
            aEliminar.MergeAttributes(attrCompartidos);
            aEliminar.SetInnerText("Eliminar");
            aEliminar.MergeAttribute("class","eliminar");
            TagBuilder aVer = new TagBuilder("a");
            aVer.SetInnerText("Ver comentario");
            aVer.MergeAttribute("class","ver");
            aVer.MergeAttributes(attrCompartidos);
            TagBuilder aEditar = new TagBuilder("a");
            aEditar.SetInnerText("Editar");
            aEditar.MergeAttribute("class","editar");
            aEditar.MergeAttributes(attrCompartidos);


            string botones = aVer+ " | " + aEditar + " | " + aEliminar;
            return botones;
        }

        private string generarCalificacion(int calificacion)
        {
            string elm = string.Empty;
            for (int i = 0; i < calificacion; i++)
            {
                TagBuilder span = new TagBuilder("span");
                span.MergeAttribute("class", "glyphicon glyphicon-star");
                
                elm += span.ToString();
            }
            return elm;
        }
    }
}