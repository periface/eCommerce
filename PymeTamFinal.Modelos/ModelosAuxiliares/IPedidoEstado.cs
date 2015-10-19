using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Modelos.ModelosAuxiliares
{
    public abstract class IPedidoEstado
    {
        public string estado { get; set; }
        public string mensaje { get; set; }
        public string color { get; set; }
        public abstract string cargaTodo();
    }
    public class pedidoEstado : IPedidoEstado
    {
        public pedidoEstado()
        {

        }
        public override string cargaTodo()
        {
            return estado + mensaje + color;
        }

    }
    public class pedidoEstadoVacio : IPedidoEstado
    {
        public override string cargaTodo()
        {
            return "Valores base no encontrados";
        }
    }
    public class implementacion
    {
        public List<pedidoEstado> estados = new List<pedidoEstado>() {
            new pedidoEstado {
                color = "rojo",
                estado = "Error",
                mensaje ="Ocurrio un error inesperado"
            }
        };
        IPedidoEstado cargaError()
        {
            var error = estados.SingleOrDefault();
            if (error == null)
                return new pedidoEstadoVacio();
            return error;
        }
        public void metodo()
        {
            var error = cargaError();
            Console.Write(error.cargaTodo());
            Console.ReadLine();
        }

    }
}
