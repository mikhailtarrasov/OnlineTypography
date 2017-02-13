namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04_Add_Job_Entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        Pay_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prices", t => t.Pay_Id)
                .Index(t => t.Pay_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Pay_Id", "dbo.Prices");
            DropIndex("dbo.Jobs", new[] { "Pay_Id" });
            DropTable("dbo.Jobs");
        }
    }
}
