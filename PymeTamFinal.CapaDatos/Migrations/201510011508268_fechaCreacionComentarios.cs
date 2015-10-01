namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fechaCreacionComentarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CajaComentarios", "fechaCreacion", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CajaComentarios", "fechaCreacion");
        }
    }
}
