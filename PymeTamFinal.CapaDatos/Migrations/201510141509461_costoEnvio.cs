namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class costoEnvio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orden", "costoEnvio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orden", "costoEnvio");
        }
    }
}
