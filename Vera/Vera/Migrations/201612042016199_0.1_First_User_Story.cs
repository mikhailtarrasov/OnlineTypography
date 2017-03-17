namespace Vera.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _01_First_User_Story : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Area = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GluePrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Format_Id = c.Int(),
                        Price_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Formats", t => t.Format_Id)
                .ForeignKey("dbo.Prices", t => t.Price_Id)
                .Index(t => t.Format_Id)
                .Index(t => t.Price_Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .Index(t => t.Currency_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GluePrices", "Price_Id", "dbo.Prices");
            DropForeignKey("dbo.Prices", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.GluePrices", "Format_Id", "dbo.Formats");
            DropIndex("dbo.Prices", new[] { "Currency_Id" });
            DropIndex("dbo.GluePrices", new[] { "Price_Id" });
            DropIndex("dbo.GluePrices", new[] { "Format_Id" });
            DropTable("dbo.Prices");
            DropTable("dbo.GluePrices");
            DropTable("dbo.Formats");
            DropTable("dbo.Currencies");
        }
    }
}
