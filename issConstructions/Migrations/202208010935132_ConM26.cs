namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RateWorks", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.RateWorks", new[] { "SiteDetails_ID" });
            DropColumn("dbo.RateWorks", "siteNameId");
            RenameColumn(table: "dbo.RateWorks", name: "SiteDetails_ID", newName: "SiteNameId");
            AddColumn("dbo.RateWorks", "siteId", c => c.Int(nullable: false));
            AlterColumn("dbo.RateWorks", "SiteNameId", c => c.Int());
            CreateIndex("dbo.RateWorks", "SiteNameId");
            AddForeignKey("dbo.RateWorks", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.RateWorks", "projectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RateWorks", "projectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RateWorks", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.RateWorks", new[] { "SiteNameId" });
            AlterColumn("dbo.RateWorks", "SiteNameId", c => c.Int());
            DropColumn("dbo.RateWorks", "siteId");
            RenameColumn(table: "dbo.RateWorks", name: "SiteNameId", newName: "SiteDetails_ID");
            AddColumn("dbo.RateWorks", "siteNameId", c => c.Int());
            CreateIndex("dbo.RateWorks", "SiteDetails_ID");
            AddForeignKey("dbo.RateWorks", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
