namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductMasters", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseRequestTables", "Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseRequestTables", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseRequestTables", "Quantity", c => c.String());
            AlterColumn("dbo.PurchaseRequestTables", "Tax", c => c.String());
            AlterColumn("dbo.ProductMasters", "Tax", c => c.String());
        }
    }
}
