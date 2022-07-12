namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "SiteNameId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "SiteNameId");
        }
    }
}
