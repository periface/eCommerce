namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "empLat", c => c.String());
            AddColumn("dbo.Empresa", "empLong", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "empLong");
            DropColumn("dbo.Empresa", "empLat");
        }
    }
}
