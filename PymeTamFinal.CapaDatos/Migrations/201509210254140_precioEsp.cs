namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precioEsp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Precios", "precioEsp", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Precios", "precioEsp");
        }
    }
}
