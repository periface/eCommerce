using PymeTamFinal.Modelos.ModelosDominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PymeTamFinal.CapaDatos
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<CajaComentarios> CajaComentarios { get; set; }
        public DbSet<CarritoDeCompra> CarritoCompra { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        //public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<CostosEnvio> CostosEnvio { get; set; }
        public DbSet<CuponDescuento> CuponDescuento { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<GaleriaProducto> GaleriaProducto { get; set; }
        public DbSet<Orden> Orden { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalle { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Politicas> Politicas { get; set; }
        public DbSet<PreguntasFrecuentes> PreguntasFrecuentes { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<RegistroActividadProducto> RegistroDeActividad { get; set; }
        public DbSet<Seccion> Seccion { get; set; }
        public DbSet<Precios> Precios { get; set; }
        public DbSet<Diferenciadores> Diferenciadores { get; set; }
        public DbSet<Mutadores> Mutadores { get; set; }
    }
}
