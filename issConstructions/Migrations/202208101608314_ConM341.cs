namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM341 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "GNameId", "dbo.Godowns");
            DropForeignKey("dbo.Issues", "SNameId", "dbo.SiteDetails");
            DropIndex("dbo.Issues", new[] { "SNameId" });
            DropIndex("dbo.Issues", new[] { "GNameId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Issues", "GNameId");
            CreateIndex("dbo.Issues", "SNameId");
            AddForeignKey("dbo.Issues", "SNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Issues", "GNameId", "dbo.Godowns", "Id", cascadeDelete: true);
        }
    }
}
