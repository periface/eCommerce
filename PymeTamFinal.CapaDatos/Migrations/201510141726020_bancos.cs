namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bancos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        bancoId = c.Int(nullable: false, identity: true),
                        bancoNombre = c.String(),
                        bancoNConvenio = c.String(),
                        bancoReferencia = c.String(),
                        bancoImagen = c.String(),
                        bancoActivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.bancoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Banco");
        }
    }
}
