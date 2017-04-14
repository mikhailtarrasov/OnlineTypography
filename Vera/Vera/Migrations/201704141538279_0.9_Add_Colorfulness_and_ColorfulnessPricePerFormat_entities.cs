namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09_Add_Colorfulness_and_ColorfulnessPricePerFormat_entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colorfulnesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ColorfulnessPricePerFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Colorfulness_Id = c.Int(),
                        Format_Id = c.Int(),
                        Price_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colorfulnesses", t => t.Colorfulness_Id)
                .ForeignKey("dbo.Formats", t => t.Format_Id)
                .ForeignKey("dbo.Prices", t => t.Price_Id)
                .Index(t => t.Colorfulness_Id)
                .Index(t => t.Format_Id)
                .Index(t => t.Price_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ColorfulnessPricePerFormats", "Price_Id", "dbo.Prices");
            DropForeignKey("dbo.ColorfulnessPricePerFormats", "Format_Id", "dbo.Formats");
            DropForeignKey("dbo.ColorfulnessPricePerFormats", "Colorfulness_Id", "dbo.Colorfulnesses");
            DropIndex("dbo.ColorfulnessPricePerFormats", new[] { "Price_Id" });
            DropIndex("dbo.ColorfulnessPricePerFormats", new[] { "Format_Id" });
            DropIndex("dbo.ColorfulnessPricePerFormats", new[] { "Colorfulness_Id" });
            DropTable("dbo.ColorfulnessPricePerFormats");
            DropTable("dbo.Colorfulnesses");
        }
    }
}
