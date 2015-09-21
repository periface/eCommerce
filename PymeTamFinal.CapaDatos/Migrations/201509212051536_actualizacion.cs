namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CajaComentarios", "revisado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CajaComentarios", "revisado");
        }
    }
}
