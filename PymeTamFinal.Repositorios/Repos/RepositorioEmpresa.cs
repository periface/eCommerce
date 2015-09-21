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
    public class RepositorioEmpresa : RepositorioBase<Empresa>
    {
        public RepositorioEmpresa(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override void Agregar(Empresa entidad)
        {
            context.Empresa.Add(entidad);
            context.SaveChanges();
        }
        public override void Editar(Empresa entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Empresa entidad)
        {
            context.Empresa.Remove(entidad);
            context.SaveChanges();
        }
        public override IQueryable<Empresa> Cargar()
        {
            return context.Empresa;
        }
        public override IQueryable<Empresa> Cargar(Expression<Func<Empresa, bool>> lambda)
        {
            return context.Empresa.Where(lambda);
        }
        public override Empresa CargarPorId(object id)
        {
            return context.Empresa.Find(id);
        }
    }
}
