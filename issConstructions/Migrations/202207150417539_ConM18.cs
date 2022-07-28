namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrders", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseOrders", new[] { "SiteDetails_ID" });
            RenameColumn(table: "dbo.PurchaseOrders", name: "SiteDetails_ID", newName: "SiteNameId");
            AddColumn("dbo.PurchaseOrders", "TotTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TotAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "Status", c => c.String());
            AlterColumn("dbo.PurchaseOrders", "SiteNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseOrders", "SiteNameId");
            AddForeignKey("dbo.PurchaseOrders", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.PurchaseOrders", "ProjectId");
            DropColumn("dbo.PurchaseOrders", "TaxAmt");
            DropColumn("dbo.PurchaseOrders", "TotalAmt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrders", "TotalAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseOrders", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseOrders", new[] { "SiteNameId" });
            AlterColumn("dbo.PurchaseOrders", "SiteNameId", c => c.Int());
            DropColumn("dbo.PurchaseOrders", "Status");
            DropColumn("dbo.PurchaseOrders", "TotAmount");
            DropColumn("dbo.PurchaseOrders", "TotTax");
            RenameColumn(table: "dbo.PurchaseOrders", name: "SiteNameId", newName: "SiteDetails_ID");
            CreateIndex("dbo.PurchaseOrders", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseOrders", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
