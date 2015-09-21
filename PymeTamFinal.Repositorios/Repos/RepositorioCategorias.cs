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
    public class RepositorioCategorias : RepositorioBase <Categoria>
    {
        public RepositorioCategorias(DataContext context):base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<Categoria> Cargar()
        {
            return context.Categoria;
        }
        public override Categoria CargarPorId(object id)
        {
            return context.Categoria.Find(id);
        }
        public override IQueryable<Categoria> Cargar(Expression<Func<Categoria, bool>> lambda)
        {
            return context.Categoria.Where(lambda);
        }
        public override void Agregar(Categoria entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Added;
            context.SaveChanges();
        }
        public override void Editar(Categoria entidad)
        {
            context.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(Categoria entidad)
        {
            //Primero descategorizamos los productos para que no se nos arroje un horroroso error con las llaves secundarias <- Click
            //Fin del comunicado
            var productos = context.Producto.Where(a => a.idCategoria == entidad.idCategoria);
            foreach (var item in productos.ToList())
            {
                item.idCategoria = null;
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            context.Entry(entidad).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }
    }
}
