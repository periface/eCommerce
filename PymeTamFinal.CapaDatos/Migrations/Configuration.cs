namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PymeTamFinal.CapaDatos.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PymeTamFinal.CapaDatos.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Categoria.AddOrUpdate(a => a.nombreCategoria,
            //    new Modelos.ModelosDominio.Categoria
            //    {
            //        nombreCategoria = "Cremas",
            //        idPadre = 0,
                   

            //    },
            //    new Modelos.ModelosDominio.Categoria()
            //    {
            //        nombreCategoria = "Ropa",
            //        idPadre = 0,
            //    });
            //context.Producto.AddOrUpdate(a => a.nombreProducto,
            //    new Modelos.ModelosDominio.Producto
            //    {
            //        descripcionCorta = "Contenido del producto",
            //        nombreProducto = "Producto nuevo",
            //        fechaCreacion = DateTime.Now,
            //        habilitado = true,
            //        imgProducto = "http://cdn-images.bundlestars.com/_images/_products/l/planetary-annihilation-new-cover-1.jpg",
            //        descripcionProducto = "Contenido web",
            //        tags = "Tag1,tag2,tag3",
            //        habilitarComentarios = true,
            //        fechaModificacion = DateTime.Now,
            //        habilitarCompraSinStock = false,
            //        mostrarSinStock = true,
            //        mostrarStock = true,
            //        slugs = "Slug",
            //        stock = 100,
            //        sku = "ASD-12345",
            //    },
            //    new Modelos.ModelosDominio.Producto
            //    {
            //        descripcionCorta = "Contenido del producto",
            //        nombreProducto = "Producto nuevo 2",
            //        fechaCreacion = DateTime.Now,
            //        habilitado = true,
            //        imgProducto = "http://cdn-images.bundlestars.com/_images/_products/l/planetary-annihilation-new-cover-1.jpg",
            //        descripcionProducto = "Contenido web",
            //        tags = "Tag1,tag2,tag3",
            //        habilitarComentarios = true,
            //        fechaModificacion = DateTime.Now,
            //        habilitarCompraSinStock = false,
            //        mostrarSinStock = true,
            //        mostrarStock = true,
            //        slugs = "Slug",
            //        stock = 100,
            //        sku = "ASD-12345"
            //    }
            // );
        }
    }
}
