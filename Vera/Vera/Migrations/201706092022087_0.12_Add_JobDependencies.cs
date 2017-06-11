namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _012_Add_JobDependencies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobDependencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Jobs", "Dependency_Id", c => c.Int());
            CreateIndex("dbo.Jobs", "Dependency_Id");
            AddForeignKey("dbo.Jobs", "Dependency_Id", "dbo.JobDependencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Dependency_Id", "dbo.JobDependencies");
            DropIndex("dbo.Jobs", new[] { "Dependency_Id" });
            DropColumn("dbo.Jobs", "Dependency_Id");
            DropTable("dbo.JobDependencies");
        }
    }
}
