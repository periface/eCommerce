using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioCliente : RepositorioBase<Cliente>
    {
        public RepositorioCliente(DataContext context): base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override Cliente CargarPorId(object id)
        {
            return context.Cliente.Find(id);
        }
        public override IQueryable<Cliente> Cargar()
        {
            return context.Cliente;
        }
    }
}
