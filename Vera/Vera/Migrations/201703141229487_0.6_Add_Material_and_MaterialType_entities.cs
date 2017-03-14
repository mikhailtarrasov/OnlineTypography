namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06_Add_Material_and_MaterialType_entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Price_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prices", t => t.Price_Id)
                .ForeignKey("dbo.MaterialTypes", t => t.Type_Id)
                .Index(t => t.Price_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.MaterialTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "Type_Id", "dbo.MaterialTypes");
            DropForeignKey("dbo.Materials", "Price_Id", "dbo.Prices");
            DropIndex("dbo.Materials", new[] { "Type_Id" });
            DropIndex("dbo.Materials", new[] { "Price_Id" });
            DropTable("dbo.MaterialTypes");
            DropTable("dbo.Materials");
        }
    }
}
