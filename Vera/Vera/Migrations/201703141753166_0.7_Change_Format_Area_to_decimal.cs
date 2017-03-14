namespace Vera.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07_Change_Format_Area_to_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Formats", "Area", c => c.Decimal(nullable: false, precision: 10, scale: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Formats", "Area", c => c.Double(nullable: false));
        }
    }
}
