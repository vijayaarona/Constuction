namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RateWorks", "RateWorkId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RateWorks", "RateWorkId");
        }
    }
}
