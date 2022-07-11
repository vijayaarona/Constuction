namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IssueID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IssueID");
        }
    }
}
