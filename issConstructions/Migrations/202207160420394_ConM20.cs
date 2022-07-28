namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.Issues", new[] { "SiteDetails_ID" });
            DropColumn("dbo.Issues", "SiteNameId");
            RenameColumn(table: "dbo.Issues", name: "SiteDetails_ID", newName: "SiteNameId");
            AddColumn("dbo.Issues", "SiteName1Id", c => c.Int());
            AlterColumn("dbo.Issues", "SiteNameId", c => c.Int());
            CreateIndex("dbo.Issues", "SiteNameId");
            AddForeignKey("dbo.Issues", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.Issues", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Issues", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.Issues", new[] { "SiteNameId" });
            AlterColumn("dbo.Issues", "SiteNameId", c => c.Int());
            DropColumn("dbo.Issues", "SiteName1Id");
            RenameColumn(table: "dbo.Issues", name: "SiteNameId", newName: "SiteDetails_ID");
            AddColumn("dbo.Issues", "SiteNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "SiteDetails_ID");
            AddForeignKey("dbo.Issues", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
