namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCommentCreatedBytobestring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "CreatedBy_Id" });
            AddColumn("dbo.Comments", "CreatedBy", c => c.String(nullable: false));
            DropColumn("dbo.Comments", "CreatedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "CreatedBy_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Comments", "CreatedBy");
            CreateIndex("dbo.Comments", "CreatedBy_Id");
            AddForeignKey("dbo.Comments", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
