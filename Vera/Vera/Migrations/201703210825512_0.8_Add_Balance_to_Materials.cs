namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08_Add_Balance_to_Materials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Materials", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Materials", "Balance");
        }
    }
}
