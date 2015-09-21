using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;
namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioPrecios : RepositorioBase<Precios>
    {
        public RepositorioPrecios(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override void Agregar(Precios entidad)
        {
            context.Precios.Add(entidad);
            context.SaveChanges();
        }
        public override Precios CargarPorId(object id)
        {
            try
            {
                int idP = (int)id;
                return context.Precios.SingleOrDefault(a => a.idProducto == idP);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public override IQueryable<Precios> Cargar(Expression<Func<Precios, bool>> lambda)
        {
            return context.Precios.Where(lambda);
        }
        public override void Editar(Precios entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Precios entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
