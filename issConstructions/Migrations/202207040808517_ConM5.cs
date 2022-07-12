namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseRequests", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "TaxAmt");
            DropColumn("dbo.PurchaseRequests", "Tax");
        }
    }
}
