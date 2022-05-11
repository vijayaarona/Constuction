namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseEntries", "grandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "discountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "grandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "discountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "dicountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "grandTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "discountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "dicountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntryTables", "discountPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntryTables", "discountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderTables", "discountPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderTables", "discountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequestTables", "discountPercent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequestTables", "discountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseEntryTables", "discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseEntryTables", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseRequestTables", "discountAmount");
            DropColumn("dbo.PurchaseRequestTables", "discountPercent");
            DropColumn("dbo.PurchaseOrderTables", "discountAmount");
            DropColumn("dbo.PurchaseOrderTables", "discountPercent");
            DropColumn("dbo.PurchaseEntryTables", "discountAmount");
            DropColumn("dbo.PurchaseEntryTables", "discountPercent");
            DropColumn("dbo.PurchaseRequests", "dicountAmount");
            DropColumn("dbo.PurchaseRequests", "discountPercentage");
            DropColumn("dbo.PurchaseRequests", "grandTotal");
            DropColumn("dbo.PurchaseOrders", "dicountAmount");
            DropColumn("dbo.PurchaseOrders", "discountPercentage");
            DropColumn("dbo.PurchaseOrders", "grandTotal");
            DropColumn("dbo.PurchaseEntries", "discountPercentage");
            DropColumn("dbo.PurchaseEntries", "grandTotal");
        }
    }
}
