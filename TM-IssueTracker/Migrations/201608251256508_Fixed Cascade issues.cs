namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCascadeissues : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Comments", name: "IssueId", newName: "Issue_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_IssueId", newName: "IX_Issue_Id");
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "Project_Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int());
            RenameIndex(table: "dbo.Comments", name: "IX_Issue_Id", newName: "IX_IssueId");
            RenameColumn(table: "dbo.Comments", name: "Issue_Id", newName: "IssueId");
            CreateIndex("dbo.Issues", "Project_Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
