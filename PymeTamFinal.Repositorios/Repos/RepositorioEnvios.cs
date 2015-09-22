using PymeTamFinal.CapaDatos;
using PymeTamFinal.Modelos.ModelosDominio;
using PymeTamFinal.Repositorios.RepoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

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
        public override CostosEnvio CargarPorId(object id)
        {
            var idEnvio = (int)id;
            var envio = context.CostosEnvio.Include("Ciudades").SingleOrDefault(a=>a.idEnvio==idEnvio);
            return envio;
        }
        public override void Editar(CostosEnvio entidad)
        {
            context.Entry(entidad).State = EntityState.Modified;
            context.SaveChanges();
        }
        public override void AgregarRelacion(int entidad0, int entidad1)
        {
            var ciudad = context.Ciudad.Find(entidad1);
            var envio = context.CostosEnvio.Include("Ciudades").SingleOrDefault(a => a.idEnvio == entidad0);
            envio.ciudades.Add(ciudad);
            context.SaveChanges();
        }
        public override void EliminarRelacion(int entidad0, int entidad1)
        {
            var ciudad = context.Ciudad.Find(entidad1);
            var envio = context.CostosEnvio.Find(entidad0);
            envio.ciudades.Remove(ciudad);
            context.SaveChanges();
        }
    }
}
