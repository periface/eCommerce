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
    public class RepositorioEstados : RepositorioBase<Estados>
    {
        public RepositorioEstados(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Estados> Cargar()
        {
            return context.Estados;
        }
        public override Estados CargarPorId(object id)
        {
            return context.Estados.Find(id);
        }
        public override IQueryable<Estados> Cargar(Expression<Func<Estados, bool>> lambda)
        {
            return context.Estados.Where(lambda);
        }
        public override void Agregar(Estados entidad)
        {
            context.Estados.Add(entidad);
            context.SaveChanges();
        }
        public override void Editar(Estados entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Estados entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
