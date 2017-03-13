namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05_Something_else : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Formats", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.FormingTypes", "Name", c => c.String(unicode: false));
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String());
            AlterColumn("dbo.FormingTypes", "Name", c => c.String());
            AlterColumn("dbo.Formats", "Name", c => c.String());
            AlterColumn("dbo.Currencies", "Name", c => c.String());
        }
    }
}
