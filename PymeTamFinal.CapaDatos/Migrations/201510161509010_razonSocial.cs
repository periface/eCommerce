namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class razonSocial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "razonSocial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "razonSocial");
        }
    }
}
