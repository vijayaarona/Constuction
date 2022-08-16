namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM35 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToolsTransfers", "ToolsName_ID", "dbo.ToolsMasters");
            DropIndex("dbo.ToolsTransfers", new[] { "ToolsName_ID" });
            DropColumn("dbo.ToolsTransfers", "ToolsId");
            RenameColumn(table: "dbo.ToolsTransfers", name: "ToolsName_ID", newName: "ToolsId");
            AddColumn("dbo.ToolsTransfers", "GodownId", c => c.Int(nullable: false));
            AddColumn("dbo.ToolsTransfers", "SNameId", c => c.Int(nullable: false));
            AddColumn("dbo.ToolsTransfers", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.ToolsTransfers", "ToolsId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToolsTransfers", "TypeId");
            CreateIndex("dbo.ToolsTransfers", "ToolsId");
            AddForeignKey("dbo.ToolsTransfers", "TypeId", "dbo.TblTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ToolsTransfers", "ToolsId", "dbo.ToolsMasters", "ID", cascadeDelete: true);
            DropColumn("dbo.ToolsTransfers", "Type");
            DropColumn("dbo.ToolsTransfers", "FLocation");
            DropColumn("dbo.ToolsTransfers", "TLocation");
          
        }
        
        public override void Down()
        {
            
            AddColumn("dbo.ToolsTransfers", "TLocation", c => c.String());
            AddColumn("dbo.ToolsTransfers", "FLocation", c => c.String());
            AddColumn("dbo.ToolsTransfers", "Type", c => c.String());
            DropForeignKey("dbo.ToolsTransfers", "ToolsId", "dbo.ToolsMasters");
            DropForeignKey("dbo.ToolsTransfers", "TypeId", "dbo.TblTypes");
            DropIndex("dbo.ToolsTransfers", new[] { "ToolsId" });
            DropIndex("dbo.ToolsTransfers", new[] { "TypeId" });
            AlterColumn("dbo.ToolsTransfers", "ToolsId", c => c.Int());
            DropColumn("dbo.ToolsTransfers", "TypeId");
            DropColumn("dbo.ToolsTransfers", "SNameId");
            DropColumn("dbo.ToolsTransfers", "GodownId");
            RenameColumn(table: "dbo.ToolsTransfers", name: "ToolsId", newName: "ToolsName_ID");
            AddColumn("dbo.ToolsTransfers", "ToolsId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToolsTransfers", "ToolsName_ID");
            AddForeignKey("dbo.ToolsTransfers", "ToolsName_ID", "dbo.ToolsMasters", "ID");
        }
    }
}
