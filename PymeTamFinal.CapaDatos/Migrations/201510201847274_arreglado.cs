namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arreglado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricoPedidoOrdens",
                c => new
                    {
                        HistoricoPedido_idHistorico = c.Int(nullable: false),
                        Orden_idOrden = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HistoricoPedido_idHistorico, t.Orden_idOrden })
                .ForeignKey("dbo.HistoricoPedido", t => t.HistoricoPedido_idHistorico, cascadeDelete: true)
                .ForeignKey("dbo.Orden", t => t.Orden_idOrden, cascadeDelete: true)
                .Index(t => t.HistoricoPedido_idHistorico)
                .Index(t => t.Orden_idOrden);
            
            DropTable("dbo.HistorialRelacion");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HistorialRelacion",
                c => new
                    {
                        idRelacion = c.Int(nullable: false, identity: true),
                        idOrden = c.Int(nullable: false),
                        idHistorico = c.Int(nullable: false),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.idRelacion);
            
            DropForeignKey("dbo.HistoricoPedidoOrdens", "Orden_idOrden", "dbo.Orden");
            DropForeignKey("dbo.HistoricoPedidoOrdens", "HistoricoPedido_idHistorico", "dbo.HistoricoPedido");
            DropIndex("dbo.HistoricoPedidoOrdens", new[] { "Orden_idOrden" });
            DropIndex("dbo.HistoricoPedidoOrdens", new[] { "HistoricoPedido_idHistorico" });
            DropTable("dbo.HistoricoPedidoOrdens");
        }
    }
}
