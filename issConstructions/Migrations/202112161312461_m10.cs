namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.paymentEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        paymentID = c.Int(nullable: false),
                        paymenttDate = c.DateTime(),
                        paymentTypeId = c.Int(nullable: false),
                        accountLedgerNameId = c.Int(nullable: false),
                        projectNameId = c.Int(nullable: false),
                        siteNameId = c.Int(nullable: false),
                        givenBy = c.String(),
                        collectBy = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        AccountLedger_ID = c.Int(),
                        SiteDetail_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountLedgerMasters", t => t.AccountLedger_ID)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetail_ID)
                .Index(t => t.AccountLedger_ID)
                .Index(t => t.SiteDetail_ID);
            
            CreateTable(
                "dbo.receiptEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        receiptID = c.Int(nullable: false),
                        receiptDate = c.DateTime(),
                        paymentTypeId = c.Int(nullable: false),
                        accountLedgerNameId = c.Int(nullable: false),
                        projectNameId = c.Int(nullable: false),
                        siteNameId = c.Int(nullable: false),
                        givenBy = c.String(),
                        collectBy = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        AccountLedger_ID = c.Int(),
                        SiteDetail_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountLedgerMasters", t => t.AccountLedger_ID)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetail_ID)
                .Index(t => t.AccountLedger_ID)
                .Index(t => t.SiteDetail_ID);
            
            AddColumn("dbo.PurchaseEntries", "ReceivedBy", c => c.String());
            AddColumn("dbo.PurchaseEntries", "Remarks", c => c.String());
            AddColumn("dbo.PurchaseEntries", "ReffBillNo", c => c.String());
            AddColumn("dbo.PurchaseEntries", "DeliveryNo", c => c.String());
            AddColumn("dbo.PurchaseEntries", "totalDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "totalTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "freightCharges", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "netAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "netAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "remarks", c => c.String());
            AddColumn("dbo.PurchaseOrders", "requestBy", c => c.String());
            AddColumn("dbo.PurchaseOrders", "orderby", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "TaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrderTables", "TaxAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseEntryTables", "discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderTables", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderTables", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderTables", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderTables", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseOrderTables", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseEntryTables", "ReceivedBy");
            DropColumn("dbo.PurchaseEntryTables", "ReffBillNo");
            DropColumn("dbo.PurchaseEntryTables", "DeliveryNo");
            DropColumn("dbo.PurchaseEntryTables", "Remarks");
            DropColumn("dbo.PurchaseEntryTables", "RequestBy");
            DropColumn("dbo.PurchaseEntryTables", "TotalTax");
            DropColumn("dbo.PurchaseEntryTables", "freightCharges");
            DropColumn("dbo.PurchaseEntryTables", "NetAmount");
            DropColumn("dbo.PurchaseOrderTables", "OrderBy");
            DropColumn("dbo.PurchaseOrderTables", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderTables", "Remarks", c => c.String());
            AddColumn("dbo.PurchaseOrderTables", "OrderBy", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "NetAmount", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "freightCharges", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "TotalTax", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "RequestBy", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "Remarks", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "DeliveryNo", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "ReffBillNo", c => c.String());
            AddColumn("dbo.PurchaseEntryTables", "ReceivedBy", c => c.String());
            DropForeignKey("dbo.receiptEntries", "SiteDetail_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.receiptEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.paymentEntries", "SiteDetail_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.paymentEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters");
            DropIndex("dbo.receiptEntries", new[] { "SiteDetail_ID" });
            DropIndex("dbo.receiptEntries", new[] { "AccountLedger_ID" });
            DropIndex("dbo.paymentEntries", new[] { "SiteDetail_ID" });
            DropIndex("dbo.paymentEntries", new[] { "AccountLedger_ID" });
            AlterColumn("dbo.PurchaseOrderTables", "TotalAmount", c => c.String());
            AlterColumn("dbo.PurchaseOrderTables", "Amount", c => c.String());
            AlterColumn("dbo.PurchaseOrderTables", "Tax", c => c.String());
            AlterColumn("dbo.PurchaseOrderTables", "Quantity", c => c.String());
            AlterColumn("dbo.PurchaseOrderTables", "Rate", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "discount", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "TotalAmount", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "Amount", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "Tax", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "Quantity", c => c.String());
            AlterColumn("dbo.PurchaseEntryTables", "Rate", c => c.String());
            DropColumn("dbo.PurchaseOrderTables", "TaxAmount");
            DropColumn("dbo.PurchaseEntryTables", "TaxAmount");
            DropColumn("dbo.PurchaseOrders", "orderby");
            DropColumn("dbo.PurchaseOrders", "requestBy");
            DropColumn("dbo.PurchaseOrders", "remarks");
            DropColumn("dbo.PurchaseOrders", "netAmount");
            DropColumn("dbo.PurchaseEntries", "netAmount");
            DropColumn("dbo.PurchaseEntries", "freightCharges");
            DropColumn("dbo.PurchaseEntries", "totalTax");
            DropColumn("dbo.PurchaseEntries", "totalDiscount");
            DropColumn("dbo.PurchaseEntries", "DeliveryNo");
            DropColumn("dbo.PurchaseEntries", "ReffBillNo");
            DropColumn("dbo.PurchaseEntries", "Remarks");
            DropColumn("dbo.PurchaseEntries", "ReceivedBy");
            DropTable("dbo.receiptEntries");
            DropTable("dbo.paymentEntries");
        }
    }
}
