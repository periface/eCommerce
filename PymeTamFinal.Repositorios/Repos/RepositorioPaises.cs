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
    public class RepositorioPaises : RepositorioBase<Pais>
    {
        public RepositorioPaises(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Pais> Cargar()
        {
            return context.Pais;
        }
        public override IQueryable<Pais> Cargar(Expression<Func<Pais, bool>> lambda)
        {
            return context.Pais.Where(lambda);
        }
        public override Pais CargarPorId(object id)
        {
            return context.Pais.Find(id);
        }
        public override void Agregar(Pais entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
        }
        public override void Editar(Pais entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Pais entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();

        }
    }
}
