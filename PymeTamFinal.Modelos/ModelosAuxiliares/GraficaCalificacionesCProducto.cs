using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public class GraficaCalificacionesCProducto
    {
        public IEnumerable<CajaComentarios> comentarios { get; set; }
        public int idProducto { get; set; }
    }
}
