namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class razonSocialCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "razonSocial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "razonSocial");
        }
    }
}
