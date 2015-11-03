namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validacionPoliticas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Politicas", "nombrePolitica", c => c.String(nullable: false));
            AlterColumn("dbo.Politicas", "contenidoPolitica", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Politicas", "contenidoPolitica", c => c.String());
            AlterColumn("dbo.Politicas", "nombrePolitica", c => c.String());
        }
    }
}
