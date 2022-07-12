namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseOrders", "TotalAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrders", "TotalAmt");
            DropColumn("dbo.PurchaseOrders", "TaxAmt");
            DropColumn("dbo.PurchaseOrders", "Tax");
        }
    }
}
