namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COnM16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseRequests", "SNameId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SNameId" });
            RenameColumn(table: "dbo.PurchaseRequests", name: "SNameId", newName: "SiteDetails_ID");
            AddColumn("dbo.PurchaseRequests", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseRequests", "SiteDetails_ID", c => c.Int());
            CreateIndex("dbo.PurchaseRequests", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetails_ID" });
            AlterColumn("dbo.PurchaseRequests", "SiteDetails_ID", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseRequests", "ProjectId");
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteDetails_ID", newName: "SNameId");
            CreateIndex("dbo.PurchaseRequests", "SNameId");
            AddForeignKey("dbo.PurchaseRequests", "SNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
        }
    }
}
