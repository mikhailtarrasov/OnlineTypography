namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _011_Add_fields_to_Material_for_paper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "SheetsPerPackage", c => c.Int());
            AddColumn("dbo.Materials", "Format_Id", c => c.Int());
            CreateIndex("dbo.Materials", "Format_Id");
            AddForeignKey("dbo.Materials", "Format_Id", "dbo.Formats", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Materials", "Format_Id", "dbo.Formats");
            DropIndex("dbo.Materials", new[] { "Format_Id" });
            DropColumn("dbo.Materials", "Format_Id");
            DropColumn("dbo.Materials", "SheetsPerPackage");
        }
    }
}
