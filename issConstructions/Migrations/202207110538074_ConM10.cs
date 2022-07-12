namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "GNameId", c => c.Int());
            CreateIndex("dbo.Issues", "GNameId");
            AddForeignKey("dbo.Issues", "GNameId", "dbo.Godowns", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "GNameId", "dbo.Godowns");
            DropIndex("dbo.Issues", new[] { "GNameId" });
            DropColumn("dbo.Issues", "GNameId");
        }
    }
}
