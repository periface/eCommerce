using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface ITransaccionExterna<T> where T :class
    {
        object GenerarContexto();
        string CargaToken();
        bool ComprobarConexion(string apiKey, string apiSecret, out string error);
        string GenerarToken(T modeloRequerido);
        bool EjecutarPago(int idPago,int idOrden,int idComprador);
    }
}
