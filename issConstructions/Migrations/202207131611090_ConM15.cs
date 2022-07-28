namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetails_ID" });
            RenameColumn(table: "dbo.PurchaseRequests", name: "SiteDetails_ID", newName: "SNameId");
            AddColumn("dbo.PurchaseRequests", "TotTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "TotAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseRequests", "SNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseRequests", "SNameId");
            AddForeignKey("dbo.PurchaseRequests", "SNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.PurchaseRequests", "ProjectId");
            DropColumn("dbo.PurchaseRequests", "TaxAmt");
            DropColumn("dbo.PurchaseRequests", "TotalAmt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "TotalAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseRequests", "SNameId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseRequests", new[] { "SNameId" });
            AlterColumn("dbo.PurchaseRequests", "SNameId", c => c.Int());
            DropColumn("dbo.PurchaseRequests", "TotAmount");
            DropColumn("dbo.PurchaseRequests", "TotTax");
            RenameColumn(table: "dbo.PurchaseRequests", name: "SNameId", newName: "SiteDetails_ID");
            CreateIndex("dbo.PurchaseRequests", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
