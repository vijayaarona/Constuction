namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COnM37 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToolsTransfers", "TId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToolsTransfers", "TId");
        }
    }
}
