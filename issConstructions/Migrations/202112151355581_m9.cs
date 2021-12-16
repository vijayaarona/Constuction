namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseRequests", "SiteDetailsId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetailsId" });
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteDetailsId", newName: "SiteDetails_ID");
            AlterColumn("dbo.PurchaseRequests", "SiteDetails_ID", c => c.Int());
            CreateIndex("dbo.PurchaseRequests", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetails_ID" });
            AlterColumn("dbo.PurchaseRequests", "SiteDetails_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteDetails_ID", newName: "SiteDetailsId");
            CreateIndex("dbo.PurchaseRequests", "SiteDetailsId");
            AddForeignKey("dbo.PurchaseRequests", "SiteDetailsId", "dbo.SiteDetails", "ID", cascadeDelete: true);
        }
    }
}
