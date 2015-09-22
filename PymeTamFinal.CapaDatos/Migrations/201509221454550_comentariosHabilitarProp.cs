namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comentariosHabilitarProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CajaComentarios", "habilitado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CajaComentarios", "habilitado");
        }
    }
}
