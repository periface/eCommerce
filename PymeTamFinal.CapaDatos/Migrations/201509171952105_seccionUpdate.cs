namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seccionUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Seccion", "estadoSeccion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Seccion", "estadoSeccion", c => c.String());
        }
    }
}
