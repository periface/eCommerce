namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modeloOrdenActualizado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orden", "ordenTipoDescuento", c => c.String());
            AddColumn("dbo.Orden", "ordenClienteComentarios", c => c.String());
            AddColumn("dbo.Orden", "enviarADirFac", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orden", "requiereFactura", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orden", "requiereFactura");
            DropColumn("dbo.Orden", "enviarADirFac");
            DropColumn("dbo.Orden", "ordenClienteComentarios");
            DropColumn("dbo.Orden", "ordenTipoDescuento");
        }
    }
}
