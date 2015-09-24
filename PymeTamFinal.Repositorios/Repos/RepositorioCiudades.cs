using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioCiudades : RepositorioBase<Ciudad>
    {
        public RepositorioCiudades(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Ciudad> Cargar()
        {
            return context.Ciudad;
        }
        public override Ciudad CargarPorId(object id)
        {
            return context.Ciudad.Find(id);
        }
        public override IQueryable<Ciudad> Cargar(Expression<Func<Ciudad, bool>> lambda)
        {
            return context.Ciudad.Include("estado").Include("estado.pais").Where(lambda);
        }
        public override void Agregar(Ciudad entidad)
        {
            context.Ciudad.Add(entidad);
            context.SaveChanges();
        }
        public override void Editar(Ciudad entidad)
        {
            context.Entry(entidad).State = EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Ciudad entidad)
        {
            context.Entry(entidad).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
