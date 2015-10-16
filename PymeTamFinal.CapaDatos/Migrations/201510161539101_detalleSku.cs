namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detalleSku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdenDetalle", "sku", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdenDetalle", "sku");
        }
    }
}
