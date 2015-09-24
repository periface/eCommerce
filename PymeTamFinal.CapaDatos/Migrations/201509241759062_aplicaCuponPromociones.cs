namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aplicaCuponPromociones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CuponDescuentoes", "usoEnDescuentos", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuponDescuentoes", "usoEnDescuentos");
        }
    }
}
