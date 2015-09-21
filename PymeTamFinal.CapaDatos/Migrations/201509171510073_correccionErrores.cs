namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correccionErrores : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "imgPrincipalEmpresa", c => c.String());
            DropColumn("dbo.Empresa", "facebookIdEmpresa");
            DropColumn("dbo.Empresa", "facebookSecretEmpresa");
            DropColumn("dbo.Empresa", "twitterIdEmpresa");
            DropColumn("dbo.Empresa", "twitterSecretEmpresa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresa", "twitterSecretEmpresa", c => c.String());
            AddColumn("dbo.Empresa", "twitterIdEmpresa", c => c.String());
            AddColumn("dbo.Empresa", "facebookSecretEmpresa", c => c.String());
            AddColumn("dbo.Empresa", "facebookIdEmpresa", c => c.String());
            DropColumn("dbo.Empresa", "imgPrincipalEmpresa");
        }
    }
}
