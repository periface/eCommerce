namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precioActualizacion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Producto", "precio_idPrecio", "dbo.Precios");
            DropIndex("dbo.Producto", new[] { "precio_idPrecio" });
            DropColumn("dbo.Producto", "precio_idPrecio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Producto", "precio_idPrecio", c => c.Int());
            CreateIndex("dbo.Producto", "precio_idPrecio");
            AddForeignKey("dbo.Producto", "precio_idPrecio", "dbo.Precios", "idPrecio");
        }
    }
}
