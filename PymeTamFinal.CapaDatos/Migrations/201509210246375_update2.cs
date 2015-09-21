namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Precios", "tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Precios", "tipo");
        }
    }
}
