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
    public class RepositorioSecciones : RepositorioBase<Seccion>
    {
        public RepositorioSecciones(DataContext context) :base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Seccion> Cargar()
        {
            return context.Seccion;
        }
        public override IQueryable<Seccion> Cargar(Expression<Func<Seccion, bool>> lambda)
        {
            return context.Seccion.Where(lambda);
        }
        public override Seccion CargarPorId(object id)
        {
            return context.Seccion.Find(id);
        }
        public override void Agregar(Seccion entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
        }
        public override void Editar(Seccion entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Seccion entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
