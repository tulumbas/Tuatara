namespace Tuatara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertBaseDateToSerial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CalendarItems", "BaseDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CalendarItems", "BaseDate", c => c.DateTime(nullable: false));
        }
    }
}
