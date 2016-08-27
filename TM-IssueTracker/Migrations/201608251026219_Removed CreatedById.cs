namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCreatedById : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            RenameColumn(table: "dbo.Issues", name: "ProjectId", newName: "Project_Id");
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int());
            CreateIndex("dbo.Issues", "Project_Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Issues", name: "Project_Id", newName: "ProjectId");
            CreateIndex("dbo.Issues", "ProjectId");
            AddForeignKey("dbo.Issues", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
