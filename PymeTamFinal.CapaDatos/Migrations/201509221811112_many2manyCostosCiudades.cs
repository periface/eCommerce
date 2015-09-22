namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class many2manyCostosCiudades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostosEnvioCiudads",
                c => new
                    {
                        CostosEnvio_idEnvio = c.Int(nullable: false),
                        Ciudad_idCiudad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CostosEnvio_idEnvio, t.Ciudad_idCiudad })
                .ForeignKey("dbo.CostosEnvio", t => t.CostosEnvio_idEnvio, cascadeDelete: true)
                .ForeignKey("dbo.Ciudad", t => t.Ciudad_idCiudad, cascadeDelete: true)
                .Index(t => t.CostosEnvio_idEnvio)
                .Index(t => t.Ciudad_idCiudad);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CostosEnvioCiudads", "Ciudad_idCiudad", "dbo.Ciudad");
            DropForeignKey("dbo.CostosEnvioCiudads", "CostosEnvio_idEnvio", "dbo.CostosEnvio");
            DropIndex("dbo.CostosEnvioCiudads", new[] { "Ciudad_idCiudad" });
            DropIndex("dbo.CostosEnvioCiudads", new[] { "CostosEnvio_idEnvio" });
            DropTable("dbo.CostosEnvioCiudads");
        }
    }
}
