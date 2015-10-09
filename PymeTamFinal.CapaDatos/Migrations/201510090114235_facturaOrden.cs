namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facturaOrden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orden", "ordenDireccionLinea1", c => c.String());
            AddColumn("dbo.Orden", "ordenDireccionLinea2", c => c.String());
            AddColumn("dbo.Orden", "ordenDireccionFACLinea1", c => c.String());
            AddColumn("dbo.Orden", "ordenDireccionFACLinea2", c => c.String());
            AddColumn("dbo.Orden", "ordenRfc", c => c.String());
            AddColumn("dbo.Orden", "razonSocial", c => c.String());
            DropColumn("dbo.Orden", "ordenDireccion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orden", "ordenDireccion", c => c.String());
            DropColumn("dbo.Orden", "razonSocial");
            DropColumn("dbo.Orden", "ordenRfc");
            DropColumn("dbo.Orden", "ordenDireccionFACLinea2");
            DropColumn("dbo.Orden", "ordenDireccionFACLinea1");
            DropColumn("dbo.Orden", "ordenDireccionLinea2");
            DropColumn("dbo.Orden", "ordenDireccionLinea1");
        }
    }
}
