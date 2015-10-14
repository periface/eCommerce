namespace PymeTamFinal.CapaDatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fechaNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orden", "ordenFechaPago", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orden", "ordenFechaPago", c => c.DateTime(nullable: false));
        }
    }
}
