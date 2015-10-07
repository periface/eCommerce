using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PymeTamFinal.Repositorios.Repos
{
    public class RepositorioEnvios : RepositorioBase<CostosEnvio>
    {
        public RepositorioEnvios(DataContext context) :base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
        public override IQueryable<CostosEnvio> Cargar()
        {
            return context.CostosEnvio;
        }
        public override IQueryable<CostosEnvio> Cargar(Expression<Func<CostosEnvio, bool>> lambda)
        {
            return context.CostosEnvio.Where(lambda);

        }
        public override void Agregar(CostosEnvio entidad)
        {
            context.CostosEnvio.Add(entidad);
            context.SaveChanges();
        }
        public override void Editar(CostosEnvio entidad)
        {
            context.Entry(entidad).State = EntityState.Modified;
            context.SaveChanges();
        }
        public override void Eliminar(CostosEnvio entidad)
        {
            context.Entry(entidad).State = EntityState.Deleted;
            context.SaveChanges();
        }
        public override CostosEnvio CargarPorId(object id)
        {
            var idEnvio = (int)id;
            var envio = context.CostosEnvio.Include("estados").SingleOrDefault(a=>a.idEnvio==idEnvio);
            return envio;
        }
        public override void AgregarRelacion(int entidad0, int entidad1)
        {
            var estado = context.Estados.Find(entidad1);
            var envio = context.CostosEnvio.Include("estados").SingleOrDefault(a => a.idEnvio == entidad0);
            envio.estados.Add(estado);
            context.SaveChanges();
        }
        public override void EliminarRelacion(int entidad0, int entidad1)
        {
            var estado = context.Estados.Find(entidad1);
            var envio = context.CostosEnvio.Include("estados").SingleOrDefault(a => a.idEnvio == entidad0);
            envio.estados.Remove(estado);
            context.SaveChanges();
        }
    }
}
