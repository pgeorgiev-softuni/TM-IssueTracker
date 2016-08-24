namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableDefinitions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 2048),
                        CreatedOn = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        CreatedBy_Id = c.String(maxLength: 128),
                        State_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.IssueStates", t => t.State_Id, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.IssueStates",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "State_Id", "dbo.IssueStates");
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Issues", new[] { "State_Id" });
            DropIndex("dbo.Issues", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            DropTable("dbo.IssueStates");
            DropTable("dbo.Projects");
            DropTable("dbo.Issues");
        }
    }
}
