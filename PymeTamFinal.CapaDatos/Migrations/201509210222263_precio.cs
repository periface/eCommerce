namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precio : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Producto", "precioProducto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Producto", "precioProducto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
