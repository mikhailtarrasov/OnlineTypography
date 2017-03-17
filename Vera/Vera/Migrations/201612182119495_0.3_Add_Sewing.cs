namespace Vera.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _03_Add_Sewing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sewings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormingType_Id = c.Int(),
                        Price_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormingTypes", t => t.FormingType_Id)
                .ForeignKey("dbo.Prices", t => t.Price_Id)
                .Index(t => t.FormingType_Id)
                .Index(t => t.Price_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sewings", "Price_Id", "dbo.Prices");
            DropForeignKey("dbo.Sewings", "FormingType_Id", "dbo.FormingTypes");
            DropIndex("dbo.Sewings", new[] { "Price_Id" });
            DropIndex("dbo.Sewings", new[] { "FormingType_Id" });
            DropTable("dbo.Sewings");
        }
    }
}
