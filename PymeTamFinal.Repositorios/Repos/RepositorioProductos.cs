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
    public class RepositorioProductos : RepositorioBase<Producto>
    {
        public RepositorioProductos(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override void Agregar(Producto entidad)
        {
           
            context.Entry(entidad).State = EntityState.Added;
            context.SaveChanges();
        }
        public override Producto CargarPorId(object id)
        {
            return context.Producto.Find(id);
        }
        public override Producto CargarPorId(object id,string[]tablasOp)
        {
            var ctxP = context.Producto;
            foreach (var item in tablasOp)
            {
                ctxP.Include(item);
            }
            var idProducto = (int)id;
            return ctxP.SingleOrDefault(a=>a.idProducto==idProducto);
        }
        public override IQueryable<Producto> Cargar()
        {
            return context.Producto.Include("Categoria");
        }
        public override IQueryable<Producto> Cargar(Expression<Func<Producto, bool>> lambda)
        {
            return context.Producto.Where(lambda).Include("Categoria");
        }
        public override void Editar(Producto entidad)
        {
            context.Entry(entidad).State = EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Producto entidad)
        {
            context.Entry(entidad).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
