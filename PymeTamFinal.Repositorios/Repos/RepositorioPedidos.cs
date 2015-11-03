using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioPedidos : RepositorioBase<Orden>
    {
        public RepositorioPedidos(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Orden> Cargar(Expression<Func<Orden, bool>> lambda)
        {
            return context.Orden.Include("cliente").Where(lambda);
        }
        public override IQueryable<Orden> Cargar()
        {
           return context.Orden.Include("cliente");

        }
        public override Orden CargarPorId(object id)
        {
            int _id = (int)id;
            return context.Orden.Include("ordenDetalle").SingleOrDefault(a=>a.idOrden==_id);
        }
    }
}
