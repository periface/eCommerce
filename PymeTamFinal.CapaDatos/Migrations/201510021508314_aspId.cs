namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aspId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "idAsp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "idAsp");
        }
    }
}
