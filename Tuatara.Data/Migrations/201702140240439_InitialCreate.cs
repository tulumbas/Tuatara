namespace Tuatara.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Priority = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Intraweek = c.Int(nullable: false),
                        ResourceID = c.Int(nullable: false),
                        WhatID = c.Int(nullable: false),
                        WhenID = c.Int(nullable: false),
                        RequestorID = c.Int(nullable: false),
                        ApproverID = c.Int(),
                        RequestedStamp = c.DateTime(nullable: false),
                        ApprovedStamp = c.DateTime(),
                        Duration = c.Double(nullable: false),
                        Description = c.String(nullable: false),
                        RTID = c.String(),
                        JiraID = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.ApproverID)
                .ForeignKey("dbo.Users", t => t.RequestorID, cascadeDelete: true)
                .ForeignKey("dbo.AssignableResources", t => t.ResourceID, cascadeDelete: true)
                .ForeignKey("dbo.Works", t => t.WhatID, cascadeDelete: true)
                .ForeignKey("dbo.CalendarItems", t => t.WhenID, cascadeDelete: true)
                .Index(t => t.ResourceID)
                .Index(t => t.WhatID)
                .Index(t => t.WhenID)
                .Index(t => t.RequestorID)
                .Index(t => t.ApproverID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DomainName = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AssignableResources",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsBookable = c.Boolean(nullable: false),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssignableResources", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Works", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.CalendarItems",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        BaseDate = c.DateTime(nullable: false),
                        WeekNo = c.Int(nullable: false),
                        PeriodType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "WhenID", "dbo.CalendarItems");
            DropForeignKey("dbo.Assignments", "WhatID", "dbo.Works");
            DropForeignKey("dbo.Works", "ParentID", "dbo.Works");
            DropForeignKey("dbo.Assignments", "ResourceID", "dbo.AssignableResources");
            DropForeignKey("dbo.AssignableResources", "ParentID", "dbo.AssignableResources");
            DropForeignKey("dbo.Assignments", "RequestorID", "dbo.Users");
            DropForeignKey("dbo.Assignments", "ApproverID", "dbo.Users");
            DropIndex("dbo.Works", new[] { "ParentID" });
            DropIndex("dbo.AssignableResources", new[] { "ParentID" });
            DropIndex("dbo.Assignments", new[] { "ApproverID" });
            DropIndex("dbo.Assignments", new[] { "RequestorID" });
            DropIndex("dbo.Assignments", new[] { "WhenID" });
            DropIndex("dbo.Assignments", new[] { "WhatID" });
            DropIndex("dbo.Assignments", new[] { "ResourceID" });
            DropTable("dbo.CalendarItems");
            DropTable("dbo.Works");
            DropTable("dbo.AssignableResources");
            DropTable("dbo.Users");
            DropTable("dbo.Assignments");
        }
    }
}
