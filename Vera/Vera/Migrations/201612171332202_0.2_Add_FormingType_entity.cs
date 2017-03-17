namespace Vera.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class _02_Add_FormingType_entity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormingTypes");
        }
    }
}
