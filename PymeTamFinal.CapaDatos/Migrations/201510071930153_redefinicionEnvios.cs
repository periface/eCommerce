namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redefinicionEnvios : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CostosEnvioCiudads", "CostosEnvio_idEnvio", "dbo.CostosEnvio");
            DropForeignKey("dbo.CostosEnvioCiudads", "Ciudad_idCiudad", "dbo.Ciudad");
            DropForeignKey("dbo.Ciudad", "idEstado", "dbo.Estados");
            DropIndex("dbo.Ciudad", new[] { "idEstado" });
            DropIndex("dbo.CostosEnvioCiudads", new[] { "CostosEnvio_idEnvio" });
            DropIndex("dbo.CostosEnvioCiudads", new[] { "Ciudad_idCiudad" });
            CreateTable(
                "dbo.EstadosCostosEnvios",
                c => new
                    {
                        Estados_idEstado = c.Int(nullable: false),
                        CostosEnvio_idEnvio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Estados_idEstado, t.CostosEnvio_idEnvio })
                .ForeignKey("dbo.Estados", t => t.Estados_idEstado, cascadeDelete: true)
                .ForeignKey("dbo.CostosEnvio", t => t.CostosEnvio_idEnvio, cascadeDelete: true)
                .Index(t => t.Estados_idEstado)
                .Index(t => t.CostosEnvio_idEnvio);
            
            AddColumn("dbo.Cliente", "ciudad", c => c.String());
            DropColumn("dbo.Cliente", "idCiudad");
            DropTable("dbo.Ciudad");
            DropTable("dbo.CostosEnvioCiudads");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CostosEnvioCiudads",
                c => new
                    {
                        CostosEnvio_idEnvio = c.Int(nullable: false),
                        Ciudad_idCiudad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CostosEnvio_idEnvio, t.Ciudad_idCiudad });
            
            CreateTable(
                "dbo.Ciudad",
                c => new
                    {
                        idCiudad = c.Int(nullable: false, identity: true),
                        nombreCiudad = c.String(nullable: false),
                        idEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idCiudad);
            
            AddColumn("dbo.Cliente", "idCiudad", c => c.Int(nullable: false));
            DropForeignKey("dbo.EstadosCostosEnvios", "CostosEnvio_idEnvio", "dbo.CostosEnvio");
            DropForeignKey("dbo.EstadosCostosEnvios", "Estados_idEstado", "dbo.Estados");
            DropIndex("dbo.EstadosCostosEnvios", new[] { "CostosEnvio_idEnvio" });
            DropIndex("dbo.EstadosCostosEnvios", new[] { "Estados_idEstado" });
            DropColumn("dbo.Cliente", "ciudad");
            DropTable("dbo.EstadosCostosEnvios");
            CreateIndex("dbo.CostosEnvioCiudads", "Ciudad_idCiudad");
            CreateIndex("dbo.CostosEnvioCiudads", "CostosEnvio_idEnvio");
            CreateIndex("dbo.Ciudad", "idEstado");
            AddForeignKey("dbo.Ciudad", "idEstado", "dbo.Estados", "idEstado", cascadeDelete: true);
            AddForeignKey("dbo.CostosEnvioCiudads", "Ciudad_idCiudad", "dbo.Ciudad", "idCiudad", cascadeDelete: true);
            AddForeignKey("dbo.CostosEnvioCiudads", "CostosEnvio_idEnvio", "dbo.CostosEnvio", "idEnvio", cascadeDelete: true);
        }
    }
}
