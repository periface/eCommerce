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
    public class RepositorioCupones : RepositorioBase<CuponDescuento>
    {
        public RepositorioCupones(DataContext context) :base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<CuponDescuento> Cargar()
        {
            return context.CuponDescuento;
        }
        public override IQueryable<CuponDescuento> Cargar(Expression<Func<CuponDescuento, bool>> lambda)
        {
            return context.CuponDescuento.Where(lambda);
        }

    }
}
