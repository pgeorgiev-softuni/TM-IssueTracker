namespace TM_IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedprojectdescriptioncolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Description", c => c.String(nullable: false, maxLength: 2048));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Description");
        }
    }
}
