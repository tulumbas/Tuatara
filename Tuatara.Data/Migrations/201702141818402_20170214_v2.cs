namespace Tuatara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170214_v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "DomainName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.AssignableResources", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Works", "ProjectName", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Works", "ProjectName", c => c.String());
            AlterColumn("dbo.AssignableResources", "Name", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "DomainName", c => c.String());
        }
    }
}
