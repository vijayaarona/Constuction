namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToolsMasters", "godownNameid", c => c.Int(nullable: false));
            AddColumn("dbo.ToolsMasters", "Godown_Id", c => c.Int());
            CreateIndex("dbo.ToolsMasters", "Godown_Id");
            AddForeignKey("dbo.ToolsMasters", "Godown_Id", "dbo.Godowns", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToolsMasters", "Godown_Id", "dbo.Godowns");
            DropIndex("dbo.ToolsMasters", new[] { "Godown_Id" });
            DropColumn("dbo.ToolsMasters", "Godown_Id");
            DropColumn("dbo.ToolsMasters", "godownNameid");
        }
    }
}
