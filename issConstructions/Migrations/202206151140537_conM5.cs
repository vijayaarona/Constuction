namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtraWorkTables", "extraWorkId", c => c.Int(nullable: false));
            AddColumn("dbo.RateWorkTables", "rateId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RateWorkTables", "rateId");
            DropColumn("dbo.ExtraWorkTables", "extraWorkId");
        }
    }
}
