namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.masterTbls", "remarks", c => c.String());
            AddColumn("dbo.masterTbls", "parentGroup", c => c.String());
            DropColumn("dbo.masterTbls", "description");
            DropColumn("dbo.masterTbls", "underGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.masterTbls", "underGroup", c => c.String());
            AddColumn("dbo.masterTbls", "description", c => c.String());
            DropColumn("dbo.masterTbls", "parentGroup");
            DropColumn("dbo.masterTbls", "remarks");
        }
    }
}
