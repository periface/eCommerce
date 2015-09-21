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
    public class RepositorioGaleria: RepositorioBase<GaleriaProducto>
    {
        public RepositorioGaleria(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override void Agregar(GaleriaProducto entidad)
        {
            context.GaleriaProducto.Add(entidad);
            context.SaveChanges();
        }
        public override void Eliminar(GaleriaProducto entidad)
        {
            context.GaleriaProducto.Remove(entidad);
            context.SaveChanges();
        }
        public override IQueryable<GaleriaProducto> Cargar(Expression<Func<GaleriaProducto, bool>> lambda)
        {
            return context.GaleriaProducto.Where(lambda);
        }
        public override IQueryable<GaleriaProducto> Cargar()
        {
            return context.GaleriaProducto;
        }
        public override GaleriaProducto CargarPorId(object id)
        {
            return context.GaleriaProducto.Find(id);
        }
    }
}
