namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IssueType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IssueType");
        }
    }
}
