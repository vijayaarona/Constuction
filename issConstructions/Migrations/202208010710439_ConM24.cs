namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "SNameId", c => c.Int(nullable: false));
            DropColumn("dbo.Issues", "SiteName1Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "SiteName1Id", c => c.Int(nullable: false));
            DropColumn("dbo.Issues", "SNameId");
        }
    }
}
