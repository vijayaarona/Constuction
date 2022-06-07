namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseEntries", "ProductNo", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrders", "ProductNo", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseRequests", "ProductNo", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseEntryTables", "ProductNo", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrderTables", "ProductNo", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseRequestTables", "ProductNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequestTables", "ProductNo");
            DropColumn("dbo.PurchaseOrderTables", "ProductNo");
            DropColumn("dbo.PurchaseEntryTables", "ProductNo");
            DropColumn("dbo.PurchaseRequests", "ProductNo");
            DropColumn("dbo.PurchaseOrders", "ProductNo");
            DropColumn("dbo.PurchaseEntries", "ProductNo");
        }
    }
}
