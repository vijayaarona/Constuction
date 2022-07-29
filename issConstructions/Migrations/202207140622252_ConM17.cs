namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetails_ID" });
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteDetails_ID", newName: "SiteNameId");
            AddColumn("dbo.PurchaseRequests", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.PurchaseRequests", "SiteNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseRequests", "SiteNameId");
            AddForeignKey("dbo.PurchaseRequests", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.PurchaseRequests", "ProjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseRequests", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteNameId" });
            AlterColumn("dbo.PurchaseRequests", "SiteNameId", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseRequests", "Status");
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteNameId", newName: "SiteDetails_ID");
            CreateIndex("dbo.PurchaseRequests", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
