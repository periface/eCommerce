namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class historico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricoPedido",
                c => new
                    {
                        idHistorico = c.Int(nullable: false, identity: true),
                        estado = c.String(),
                        colorEstado = c.String(),
                        fecha = c.DateTime(nullable: false),
                        comentarios = c.String(),
                    })
                .PrimaryKey(t => t.idHistorico);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HistorialRelacion");
            DropTable("dbo.HistoricoPedido");
        }
    }
}
