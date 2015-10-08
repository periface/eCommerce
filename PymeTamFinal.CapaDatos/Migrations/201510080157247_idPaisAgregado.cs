namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idPaisAgregado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "idPais", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "idPais");
        }
    }
}
