namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idEstadoAgregado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "idEstado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "idEstado");
        }
    }
}
