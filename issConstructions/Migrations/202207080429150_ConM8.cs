namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseEntries", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "TaxAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "TotalAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseEntries", "PurchaseType", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseEntries", "PurchaseType");
            DropColumn("dbo.PurchaseEntries", "TotalAmt");
            DropColumn("dbo.PurchaseEntries", "TaxAmt");
            DropColumn("dbo.PurchaseEntries", "Tax");
        }
    }
}
