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
    public class RepositorioComentarios : RepositorioBase<CajaComentarios>
    {
        public RepositorioComentarios(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<CajaComentarios> Cargar()
        {
            return context.CajaComentarios;
        }
        public override IQueryable<CajaComentarios> Cargar(Expression<Func<CajaComentarios, bool>> lambda)
        {
            return context.CajaComentarios.Where(lambda);
        }
        public override void Eliminar(CajaComentarios entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }
        public override void Editar(CajaComentarios entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
