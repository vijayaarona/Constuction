namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToolsTransfers", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ToolsTransfers", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.ToolsTransfers", "UpdateBy", c => c.String());
            AddColumn("dbo.ToolsTransfers", "UpdatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToolsTransfers", "UpdatedDate");
            DropColumn("dbo.ToolsTransfers", "UpdateBy");
            DropColumn("dbo.ToolsTransfers", "CreatedDate");
            DropColumn("dbo.ToolsTransfers", "isDeleted");
        }
    }
}
