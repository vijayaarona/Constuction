namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COnM19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseEntries", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseEntries", new[] { "SiteDetails_ID" });
            RenameColumn(table: "dbo.PurchaseEntries", name: "SiteDetails_ID", newName: "SiteNameId");
            AddColumn("dbo.PurchaseEntries", "TotTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "TotAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntries", "SiteNameId", c => c.Int(nullable: false));
            CreateIndex("dbo.PurchaseEntries", "SiteNameId");
            AddForeignKey("dbo.PurchaseEntries", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.PurchaseEntries", "ProjectId");
            DropColumn("dbo.PurchaseEntries", "TaxAmt");
            DropColumn("dbo.PurchaseEntries", "TotalAmt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseEntries", "TotalAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "ProjectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PurchaseEntries", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.PurchaseEntries", new[] { "SiteNameId" });
            AlterColumn("dbo.PurchaseEntries", "SiteNameId", c => c.Int());
            DropColumn("dbo.PurchaseEntries", "TotAmount");
            DropColumn("dbo.PurchaseEntries", "TotTax");
            RenameColumn(table: "dbo.PurchaseEntries", name: "SiteNameId", newName: "SiteDetails_ID");
            CreateIndex("dbo.PurchaseEntries", "SiteDetails_ID");
            AddForeignKey("dbo.PurchaseEntries", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
