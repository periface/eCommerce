namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionActualizada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CajaComentarios", "nombreCliente", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CajaComentarios", "nombreCliente");
        }
    }
}
