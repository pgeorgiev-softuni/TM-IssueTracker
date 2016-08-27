namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Projects", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.Issues", "CreatedBy_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Projects", "CreatedBy_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Issues", "CreatedBy_Id");
            CreateIndex("dbo.Projects", "CreatedBy_Id");
            AddForeignKey("dbo.Projects", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Issues", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.Projects", "CreatedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Issues", "CreatedBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Projects", "CreatedBy_Id");
            CreateIndex("dbo.Issues", "CreatedBy_Id");
            AddForeignKey("dbo.Projects", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
