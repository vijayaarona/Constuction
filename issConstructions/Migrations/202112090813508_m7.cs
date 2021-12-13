namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "NetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "RequestBy", c => c.String());
            AddColumn("dbo.PurchaseRequests", "Remarks", c => c.String());
            AlterColumn("dbo.PurchaseRequestTables", "Quantity", c => c.String());
            DropColumn("dbo.PurchaseRequestTables", "NetAmount");
            DropColumn("dbo.PurchaseRequestTables", "RequestBy");
            DropColumn("dbo.PurchaseRequestTables", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequestTables", "Remarks", c => c.String());
            AddColumn("dbo.PurchaseRequestTables", "RequestBy", c => c.String());
            AddColumn("dbo.PurchaseRequestTables", "NetAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseRequestTables", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseRequests", "Remarks");
            DropColumn("dbo.PurchaseRequests", "RequestBy");
            DropColumn("dbo.PurchaseRequests", "NetAmount");
        }
    }
}
