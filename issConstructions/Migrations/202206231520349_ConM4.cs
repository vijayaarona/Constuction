namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToolsMasters", "Godown_Id", "dbo.Godowns");
            DropIndex("dbo.ToolsMasters", new[] { "Godown_Id" });
            RenameColumn(table: "dbo.ToolsMasters", name: "Godown_Id", newName: "GNameId");
            AlterColumn("dbo.ToolsMasters", "GNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToolsMasters", "GNameId");
            AddForeignKey("dbo.ToolsMasters", "GNameId", "dbo.Godowns", "Id", cascadeDelete: true);
            DropColumn("dbo.ToolsMasters", "godownNameid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToolsMasters", "godownNameid", c => c.Int(nullable: false));
            DropForeignKey("dbo.ToolsMasters", "GNameId", "dbo.Godowns");
            DropIndex("dbo.ToolsMasters", new[] { "GNameId" });
            AlterColumn("dbo.ToolsMasters", "GNameId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ToolsMasters", name: "GNameId", newName: "Godown_Id");
            CreateIndex("dbo.ToolsMasters", "Godown_Id");
            AddForeignKey("dbo.ToolsMasters", "Godown_Id", "dbo.Godowns", "Id");
        }
    }
}
