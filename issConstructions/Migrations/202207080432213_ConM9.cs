namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseEntries", "PurType", c => c.String());
            DropColumn("dbo.PurchaseEntries", "PurchaseType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseEntries", "PurchaseType", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseEntries", "PurType");
        }
    }
}
