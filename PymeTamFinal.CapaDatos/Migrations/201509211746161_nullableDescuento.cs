namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDescuento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Precios", "descuento", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Precios", "descuento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
