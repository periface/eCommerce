using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Contratos.Repositorio
{
    public interface IPaypalCryptBase<T> where T :class
    {
        string Encriptar(string valor,bool usarHash);
        string Desencriptar(string valor,bool usarHash);
        void Guardar(T model);
    }
}
