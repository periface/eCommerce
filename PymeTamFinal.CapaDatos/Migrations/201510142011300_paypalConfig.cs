namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paypalConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaypalConfig",
                c => new
                    {
                        idCuenta = c.Int(nullable: false, identity: true),
                        habilitada = c.Boolean(nullable: false),
                        secret = c.String(),
                        appId = c.String(),
                        emailCuenta = c.String(),
                    })
                .PrimaryKey(t => t.idCuenta);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaypalConfig");
        }
    }
}
